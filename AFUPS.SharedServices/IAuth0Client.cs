using System;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;
using Microsoft.AspNetCore.Components;

namespace AFUPS.SharedServices
{
    public interface IAuth0Client
    {
        IdentityModel.OidcClient.Browser.IBrowser Browser { get; set; }
        Task<LoginResult> LoginAsync();
        Task<BrowserResult> LogoutAsync();
        event Action AuthClientEventCallback;
    }
}

