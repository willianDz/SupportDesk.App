using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.App.Exceptions;
using SupportDesk.App.Models.User;
using SupportDesk.App.Services.AppEnvironment;
using SupportDesk.App.Services.Dialog;
using SupportDesk.App.Services.Navigation;
using SupportDesk.App.Services.Settings;
using SupportDesk.App.ViewModels.Base;

namespace SupportDesk.App.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        [ObservableProperty]
        private User userInfo;

        [ObservableProperty]
        private UserSummary userSummary;

        [ObservableProperty]
        private bool isRefreshing;

        [RelayCommand]
        async Task Refresh()
        {
            IsRefreshing = true;
            await InitializeAsync();
            IsRefreshing = false;
        }

        private readonly IAppEnvironmentService _appEnvironmentService;
        private readonly IConnectivity _connectivity;

        public HomeViewModel(
            IAppEnvironmentService appEnvironmentService,
            IConnectivity connectivity,
            IDialogAppService dialogService,
            INavigationAppService navigationService,
            ISettingsService settingsService) : base(dialogService, navigationService, settingsService)
        {
            _appEnvironmentService = appEnvironmentService;
            _connectivity = connectivity;
        }

        public override async Task InitializeAsync()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                ErrorNoInternet();
                return;
            }

            SetInitializingIndicators(true, "Cargando resumen");

            await IsBusyFor(async () =>
            {
                await LoadUserInfo();
                await LoadUserSummary();
            });

            SetInitializingIndicators(false);

            await base.InitializeAsync();
        }

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();

            if (UserInfo == null ||
                UserInfo.Id == Guid.Empty ||
                UserInfo.Id != SettingsService.UserId &&
                IsInitialized)
            {
                await InitializeAsync();
            }
        }

        private async Task LoadUserInfo()
        {
            try
            {
                var response = await _appEnvironmentService
                    .UserService
                    .GetUserInfo();

                if (response.Success)
                {
                    UserInfo = response.User;
                }
                else
                {
                    string message = "No se pudo cargar la información del usuario.";

                    SetErrorState(message);
                }
            }
            catch (Exception)
            {
                SetErrorState(
                        "Ocurrió un error al cargar la información del usuario");
            }
        }

        private async Task LoadUserSummary()
        {
            try
            {
                var response = await _appEnvironmentService
                   .UserService
                   .GetUserSummary();

                if (response.Success)
                {
                    UserSummary = new UserSummary()
                    {
                        ApprovedRequests = response.ApprovedRequests,
                        PendingRequests = response.PendingRequests,
                        RejectedRequests = response.RejectedRequests,
                        TotalRequests = response.TotalRequests
                    };
                }
                else
                {
                    string message = "No se pudo cargar el resumen del usuario.";

                    SetErrorState(message);
                }
            }
            catch (InternetConnectionException)
            {
                ErrorNoInternet();
            }
            catch (Exception)
            {
                SetErrorState(
                    "Ocurrió un error al cargar el resumen del mes");
            }
        }
    }
}