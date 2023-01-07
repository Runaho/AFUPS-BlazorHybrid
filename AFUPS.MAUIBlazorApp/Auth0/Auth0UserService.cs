using System;
using System.Text.Json;
using AFUPS.Data;
using IdentityModel.OidcClient;

namespace AFUPS.MAUIBlazorApp.Auth0
{
    public class Auth0UserService : IAuth0UserService
    {
        public static Auth0User Auth0User { get; set; } = new Auth0User();
        public IDataContext DataContext { get; }

        public Auth0UserService(IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public void SaveUser(LoginResult user)
        {
            //DataContext.SaveUser(JsonSerializer.Serialize<LoginResult>(user));
            Auth0User.user = user;
        }

        public async Task<Auth0User> FetchUserAsync()
        {
            string auth0User = await SecureStorage.Default.GetAsync("Auth0User");
            if (!string.IsNullOrWhiteSpace(auth0User))
                Auth0User.user = JsonSerializer.Deserialize<LoginResult>(DataContext.GetUser());

            return Get0User();
        }

        public Auth0User Get0User()
        {
            return Auth0User;
        }

        public bool IsLoggedIn()
        {
            return Auth0User.user?.User != null;
        }
    }
}

