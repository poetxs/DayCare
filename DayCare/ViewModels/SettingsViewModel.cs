using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DayCare.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DayCare.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    private readonly IAuthService _authService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private string _userName = string.Empty;

    [ObservableProperty]
    private string _userEmail = string.Empty;

    [ObservableProperty]
    private bool _notificationsEnabled = true;

    [ObservableProperty]
    private bool _darkModeEnabled;

    public SettingsViewModel(IAuthService authService, IServiceProvider serviceProvider)
    {
        _authService = authService;
        _serviceProvider = serviceProvider;
        Title = "Settings";
        LoadUserInfo();
    }

    private void LoadUserInfo()
    {
        var user = _authService.CurrentUser;
        if (user != null)
        {
            UserName = user.DisplayName;
            UserEmail = user.Email;
        }
    }

    [RelayCommand]
    private async Task SignOutAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            await _authService.SignOutAsync();

            // Navigate back to login
            Application.Current!.Windows[0].Page =
                new NavigationPage(_serviceProvider.GetRequiredService<Views.LoginPage>());
        }
        finally
        {
            IsBusy = false;
        }
    }
}
