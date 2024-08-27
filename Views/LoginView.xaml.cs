using SupportDesk.App.ViewModels.Base;

namespace SupportDesk.App.Views;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        var content = Content;
        Content = null;
        Content = content;

        if (BindingContext is IViewModelBase ivmb)
        {
            if (!ivmb.IsInitialized)
            {
                ivmb.IsInitialized = true;
                await ivmb.InitializeAsync();
            }

            await ivmb.OnAppearingAsync();
        }
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();

        if (BindingContext is IViewModelBase ivmb)
        {
            await ivmb.OnDisappearingAsync();
        }
    }
}