using System;
using IdentityModel.OidcClient;

namespace AFUPS.Data
{
    public interface IAuth0UserService
    {
        void SaveUser(LoginResult user);
        Task<Auth0User> FetchUserAsync();
        Auth0User Get0User();
        bool IsLoggedIn();
    }
}

