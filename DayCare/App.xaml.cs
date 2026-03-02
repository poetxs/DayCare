using DayCare.Views;

namespace DayCare;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        MainPage = new NavigationPage(serviceProvider.GetRequiredService<LoginPage>());
    }
}
