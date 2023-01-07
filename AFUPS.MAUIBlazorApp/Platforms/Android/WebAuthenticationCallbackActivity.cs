using System;
using Android.App;
using Android.Content.PM;
using Android.Webkit;

namespace AFUPS.MAUIBlazorApp.Platforms.Android
{

    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
    [IntentFilter(new[] { "" },
              DataScheme = CALLBACK_SCHEME)]
    public class WebAuthenticationCallbackActivity : Microsoft.Maui.Authentication.WebAuthenticatorCallbackActivity
    {
        const string CALLBACK_SCHEME = "afupmaui";
    }
}

