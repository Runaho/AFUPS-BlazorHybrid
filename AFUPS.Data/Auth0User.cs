using System;
using IdentityModel.OidcClient;
using System.Security.Claims;
using SQLite;

namespace AFUPS.Data
{
    public class Auth0User
    {
        public LoginResult user;

        public string AccessToken => user.AccessToken;
        public string IdentityToken => user.IdentityToken;
        public string RefreshToken => user.RefreshToken;
        public string Name => user.User.FindFirst(c => c.Type == "name")?.Value;
        public string Email => user.User.FindFirst(c => c.Type == "name")?.Value;
        public string Picture => user.User.FindFirst(c => c.Type == "picture")?.Value;
        public IEnumerable<Claim> Claims => user.User.Claims;
    }
}

