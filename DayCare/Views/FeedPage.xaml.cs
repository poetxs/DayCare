using DayCare.ViewModels;

namespace DayCare.Views;

public partial class FeedPage : ContentPage
{
    public FeedPage(FeedViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
