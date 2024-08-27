using SupportDesk.App.Views.Base;

namespace SupportDesk.App.Views;

public partial class HomeView : ContentPageBase
{
    public HomeView(HomeViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}