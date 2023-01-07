using System;
using IdentityModel.OidcClient.Browser;

namespace AFUPS.SharedServices
{
    public interface IWebAuthenticator
    {
        Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default);
     }
}

