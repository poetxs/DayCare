using DayCare.Services;
using DayCare.ViewModels;
using DayCare.Views;
using Microsoft.Extensions.Logging;

namespace DayCare;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register services
        builder.Services.AddSingleton<IAuthService, MsalAuthService>();

        // Register AppShell
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<PickupDropoffViewModel>();
        builder.Services.AddTransient<FeedViewModel>();
        builder.Services.AddTransient<BillingViewModel>();
        builder.Services.AddTransient<SettingsViewModel>();

        // Register Views
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<PickupDropoffPage>();
        builder.Services.AddTransient<FeedPage>();
        builder.Services.AddTransient<BillingPage>();
        builder.Services.AddTransient<SettingsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
