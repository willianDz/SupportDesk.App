using SupportDesk.App.ViewModels.Base;

namespace SupportDesk.App.Views.Base;

public abstract class ContentPageBase : PageBase
{
    protected ContentPageBase()
    {
        NavigationPage.SetBackButtonTitle(this, string.Empty);
    }

    protected override async void OnAppearing()
    {
        if (BindingContext is IViewModelBase ivmb)
        {
            if (!ivmb.IsInitialized)
            {
                await Task.Delay(200);

                ivmb.IsInitialized = true;

                await ivmb.InitializeAsync();
            }

            await ivmb.OnAppearingAsync();
        }

        base.OnAppearing();
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
