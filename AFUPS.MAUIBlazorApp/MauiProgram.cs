using AFUPS.Data;
using AFUPS.MAUIBlazorApp.Auth0;
using AFUPS.MAUIBlazorApp.Services;
using AFUPS.SharedServices;

namespace AFUPS.MAUIBlazorApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        builder.Services.AddTransient<IDataContext, SqliteDataContext>();
        builder.Services.AddTransient<IUserArchives, UserArchives>();
        builder.Services.AddTransient<IUserUploads, UserUploads>();
        builder.Services.AddScoped<IConsent, ConsentService>();

        builder.Services.AddTransient<IUploaderProviders, UploaderProviders>();
        builder.Services.AddTransient<AppDefaults>();
        builder.Services.AddSingleton<IFileProcessManager, FileProcessManager>();

        builder.Services.AddSingleton<IShareVia, ShareVia>();
        builder.Services.AddSingleton<IPromptService, PromptService>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<IAuth0UserService, Auth0UserService>(c => new Auth0UserService(c.GetService<IDataContext>()));

        builder.Services.AddSingleton<IAuth0Client>(c => new Auth0Client(new()
        {
            Domain = "domain",
            ClientId = "clientId",
            Scope = "openid profile",
            RedirectUri = "afupmaui://callback"
        }, c.GetService<IAuth0UserService>()));


        return builder.Build();
    }
}

