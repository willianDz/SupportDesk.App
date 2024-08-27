using CommunityToolkit.Mvvm.Input;
using SupportDesk.App.Services.Dialog;
using SupportDesk.App.Services.Navigation;
using SupportDesk.App.Services.Settings;
using SupportDesk.App.ViewModels.Base;
using SupportDesk.App.Views;

namespace SupportDesk.App.ViewModels;

public partial class AppShellViewModel : ViewModelBase
{
    [RelayCommand]
    async Task AboutApp()
    {
        await NavigationService.NavigateToAsync(nameof(AboutAppView));
    }

    [RelayCommand]
    async Task LogOut()
    {
        await PerformLogOut();
    }

    public AppShellViewModel(
        IDialogAppService dialogService,
        INavigationAppService navigationService,
        ISettingsService settingsService) : base(dialogService, navigationService, settingsService)
    {
    }

    private async Task PerformLogOut()
    {
        try
        {

            SettingsService.AuthAccessToken = string.Empty;
            SettingsService.UserId = Guid.Empty;

            await NavigationService.NavigateToAsync($"//{nameof(LoginView)}");
        }
        catch (Exception)
        {
            await DialogService.ShowAlertAsync(
                "Ocurrió un error al cerrar sesión. Por favor vuelva a intentarlo.",
                "Atención",
                "Aceptar");
        }
    }
}
