using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DayCare.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DayCare.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IAuthService _authService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _hasError;

    public LoginViewModel(IAuthService authService, IServiceProvider serviceProvider)
    {
        _authService = authService;
        _serviceProvider = serviceProvider;
        Title = "DayCare Login";
    }

    [RelayCommand]
    private async Task SignInAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            HasError = false;
            ErrorMessage = string.Empty;

            var user = await _authService.SignInAsync();

            if (user != null)
            {
#if PLATFORM
                // Navigate to the main shell with tabs
                Application.Current!.Windows[0].Page = _serviceProvider.GetRequiredService<AppShell>();
#endif
            }
            else
            {
                HasError = true;
                ErrorMessage = "Sign-in failed. Please try again.";
            }
        }
        catch (Exception ex)
        {
            HasError = true;
            ErrorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
