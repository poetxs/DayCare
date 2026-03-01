using DayCare.ViewModels;

namespace DayCare.Views;

public partial class PickupDropoffPage : ContentPage
{
    public PickupDropoffPage(PickupDropoffViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
