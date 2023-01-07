using System;
using AFUPS.SharedServices;

namespace AFUPS.MAUIBlazorApp.Services
{
	public class ShareVia : IShareVia
	{
        public async Task ShareUri(string uri)
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share My Archive"
            });
        }
    }
}

