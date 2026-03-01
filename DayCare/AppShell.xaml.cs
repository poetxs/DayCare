using DayCare.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DayCare;

public partial class AppShell : Shell
{
    private readonly IServiceProvider _serviceProvider;

    public AppShell(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();

        // Set page content via DI so ViewModels are properly injected
        PickupDropoffContent.Content = _serviceProvider.GetRequiredService<PickupDropoffPage>();
        FeedContent.Content = _serviceProvider.GetRequiredService<FeedPage>();
        BillingContent.Content = _serviceProvider.GetRequiredService<BillingPage>();

        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }
}
