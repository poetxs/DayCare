using DayCare.Services;
using DayCare.Views;

namespace DayCare;

public partial class App : Application
{
    public App(IAuthService authService, LoginPage loginPage)
    {
        InitializeComponent();
        MainPage = new NavigationPage(loginPage);
    }
}
