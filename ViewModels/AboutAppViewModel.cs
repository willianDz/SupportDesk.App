using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.App.Services.Dialog;
using SupportDesk.App.Services.Navigation;
using SupportDesk.App.Services.Settings;
using SupportDesk.App.ViewModels.Base;

namespace SupportDesk.App.ViewModels
{
    public partial class AboutAppViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string disclimerTitle;

        [ObservableProperty]
        private string disclimer;

        [ObservableProperty]
        private string versionApp;

        [RelayCommand]
        async Task Close()
        {
            try
            {
                await NavigationService.PopAsync();
            }
            catch (Exception)
            {
                await DialogService.ShowAlertAsync(
                   "Ocurrió un error. Por favor vuelva a intentarlo.",
                   "Atención",
                   "Aceptar");
            }
        }

        [RelayCommand]
        async Task ShareApp()
        {
            await ShareAppLink();
        }

        private readonly IShare _share;

        public AboutAppViewModel(
            IShare share,
            IDialogAppService dialogService,
            INavigationAppService navigationService,
            ISettingsService settingsService) : base(dialogService, navigationService, settingsService)
        {
            _share = share;
        }

        public override async Task InitializeAsync()
        {
            await Task.Run(() =>
            {
                DisclimerTitle = GlobalSetting.DisclaimerTitle;
                Disclimer = GlobalSetting.Disclaimer;
                VersionApp = VersionTracking.Default.CurrentVersion;
            });
        }

        private async Task ShareAppLink()
        {
            try
            {
                await _share.RequestAsync(new ShareTextRequest
                {
                    Uri = GlobalSetting.AppStoreUrl,
                    Title = GlobalSetting.ShareAppMessage,
                    Subject = GlobalSetting.ShareAppMessage,
                    Text = GlobalSetting.ShareAppMessage,
                });
            }
            catch (Exception)
            {
                await DialogService.ShowAlertAsync(
                    "Ocurrió un error. Por favor vuelva a intentarlo.",
                    "Atención",
                    "Aceptar");
            }
        }
    }
}
