using System;
using AFUPS.Data;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Microsoft.AspNetCore.Components;

namespace AFUPS.MAUIBlazorApp.Auth0
{
    public class Auth0Client : SharedServices.IAuth0Client
    {
        private readonly OidcClient oidcClient;

        public event Action AuthClientEventCallback;

        private IAuth0UserService Auth0UserService { get; set; }

        public Auth0Client(Auth0ClientOptions options, IAuth0UserService auth0UserService)
        {
            oidcClient = new OidcClient(new OidcClientOptions
            {
                Authority = $"https://{options.Domain}",
                ClientId = options.ClientId,
                Scope = options.Scope,
                RedirectUri = options.RedirectUri,
                Browser = options.Browser
            });
            Auth0UserService = auth0UserService;
        }

        public IdentityModel.OidcClient.Browser.IBrowser Browser
        {
            get
            {
                return oidcClient.Options.Browser;
            }
            set
            {
                oidcClient.Options.Browser = value;
            }
        }

        public async Task<LoginResult> LoginAsync()
        {
            var oidcResult = await oidcClient.LoginAsync();
            Auth0UserService.SaveUser(oidcResult);
            AuthClientEventCallback.Invoke();
            return oidcResult;
        }

        public async Task<BrowserResult> LogoutAsync()
        {
            var logoutParameters = new Dictionary<string, string>
            {
              {"client_id", oidcClient.Options.ClientId },
              {"returnTo", oidcClient.Options.RedirectUri }
            };

            var logoutRequest = new LogoutRequest();
            var endSessionUrl = new RequestUrl($"{oidcClient.Options.Authority}/v2/logout")
              .Create(new Parameters(logoutParameters));
            var browserOptions = new BrowserOptions(endSessionUrl, oidcClient.Options.RedirectUri)
            {
                Timeout = TimeSpan.FromSeconds(logoutRequest.BrowserTimeout),
                DisplayMode = logoutRequest.BrowserDisplayMode
            };

            var browserResult = await oidcClient.Options.Browser.InvokeAsync(browserOptions);
            Auth0UserService.SaveUser(new LoginResult { });
            AuthClientEventCallback.Invoke();
            return browserResult;
        }


    }
}

