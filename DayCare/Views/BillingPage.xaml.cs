using DayCare.ViewModels;

namespace DayCare.Views;

public partial class BillingPage : ContentPage
{
    public BillingPage(BillingViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
