using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SupportDesk.App.Exceptions;
using SupportDesk.App.Extensions;
using SupportDesk.App.Models.Auth;
using SupportDesk.App.Services.Dialog;
using SupportDesk.App.Services.Login;
using SupportDesk.App.Services.Navigation;
using SupportDesk.App.Services.Settings;
using SupportDesk.App.Validations;
using SupportDesk.App.ViewModels.Base;
using SupportDesk.App.Views;

namespace SupportDesk.App.ViewModels
{
    [QueryProperty(nameof(Email), "email")]
    public partial class VerifyTwoFactorAuthenticationViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly ILoginService _loginService;
        private readonly IConnectivity _connectivity;

        public ValidatableObject<string> TwoFactorCode { get; private set; }

        [ObservableProperty]
        private bool isValid;

        [ObservableProperty]
        private bool isLogin;

        [ObservableProperty]
        private string email;

        [RelayCommand]
        async Task VerifyCode()
        {
            await IsBusyFor(VerifyCodeAsync);
        }

        [RelayCommand]
        void Validate()
        {
            ValidateInputsValues();
        }

        public VerifyTwoFactorAuthenticationViewModel(
            IDialogAppService dialogService,
            INavigationAppService navigationService,
            ISettingsService settingsService,
            ILoginService loginService,
            IConnectivity connectivity)
            : base(dialogService, navigationService, settingsService)
        {
            _settingsService = settingsService;
            _loginService = loginService;
            _connectivity = connectivity;

            TwoFactorCode = new ValidatableObject<string>();

            AddValidations();
        }

        public override void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            base.ApplyQueryAttributes(query);

            if (query.ValueAsBool("Logout"))
            {
                ClearValues();
            }
        }

        public override async Task InitializeAsync()
        {
            ClearValues();

            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await ShowToast("No hay conexion a internet");
            }

            await base.InitializeAsync();
        }

        public override async Task OnAppearingAsync()
        {
            await InitializeAsync();

            await base.OnAppearingAsync();
        }

        private async Task VerifyCodeAsync()
        {
            try
            {
                IsValid = ValidateInputsValues();

                if (!IsValid)
                {
                    return;
                }

                bool isConnected = await IsConnected();

                if (!isConnected)
                {
                    return;
                }

                var request = new VerifyTwoFactorRequest()
                {
                    Email = Email,
                    TwoFactorCode = TwoFactorCode.Value,
                };

                VerifyTwoFactorResponse response = await _loginService.VerifyTwoFactorCode(request);

                bool successfullogin = response != null
                    && response.Success
                    && !string.IsNullOrEmpty(response.JwtToken);

                if (successfullogin)
                {
                    GetAuthenticationToken(response);

                    await Navigate();
                }
                else
                {
                    string message = response!.GetErrorMessage();

                    await DialogService.ShowAlertAsync(
                        message,
                        "Atención",
                        "Aceptar");
                }
            }
            catch (InternetConnectionException)
            {
                await ShowToast("No hay conexion a internet");
            }
            catch (Exception ex)
            {
                await DialogService.ShowAlertAsync(
                    "Ocurrió un error al realizar la verificación. Por favor vuelva a intentarlo." + ex.Message.ToString(),
                    "Atención",
                    "Aceptar");
            }
        }

        private void GetAuthenticationToken(VerifyTwoFactorResponse? response)
        {
            UserToken? payload = StringExtensions.ExtractUserInfoFromToken(response!.JwtToken);

            if (payload is null)
            {
                throw new ArgumentException(nameof(payload));
            }

            _settingsService.AuthAccessToken = payload.Token;
            _settingsService.UserId = payload.UserId;
        }

        private async Task Navigate()
        {
            var parameters = new Dictionary<string, object>
            {
                { "email", TwoFactorCode.Value },
            };

            await NavigationService.NavigateToAsync($"//{nameof(HomeView)}", parameters);
        }

        private async Task<bool> IsConnected()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await ShowToast("No hay conexion a internet");
                return false;
            }

            return true;
        }

        private bool ValidateInputsValues()
        {
            return TwoFactorCode.Validate();
        }

        private void AddValidations()
        {
            TwoFactorCode.Validations.Add(
                new IsNotNullOrEmptyRule<string>
                {
                    ValidationMessage = "El código es requerido."
                });
        }

        private void ClearValues()
        {
            TwoFactorCode.Reset();

            IsValid = true;
            IsLogin = false;
        }
    }
}