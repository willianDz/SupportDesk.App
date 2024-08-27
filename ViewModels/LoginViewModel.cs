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
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly ILoginService _loginService;
        private readonly IConnectivity _connectivity;

        public ValidatableObject<string> UserName { get; private set; }

        public ValidatableObject<string> Password { get; private set; }

        [ObservableProperty]
        private bool isValid;

        [ObservableProperty]
        private bool isLogin;

        [RelayCommand]
        async Task SignIn()
        {
            await IsBusyFor(SignInAsync);
        }

        [RelayCommand]
        void Validate()
        {
            ValidateInputsValues();
        }

        public LoginViewModel(
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

            UserName = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();

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

        private async Task SignInAsync()
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

                var request = new LoginRequest()
                {
                    Email = UserName.Value,
                    Password = Password.Value,
                };

                LoginResponse response = await _loginService.Login(request);

                bool successfullogin = response != null
                    && response.Success
                    && response.RequiresTwoFactor;

                if (successfullogin)
                {
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
                    "Ocurrió un error al iniciar sesión. Por favor vuelva a intentarlo." + ex.Message.ToString(),
                    "Atención",
                    "Aceptar");
            }
        }                

        private async Task Navigate()
        {
            var parameters = new Dictionary<string, object>
            {
                { "email", UserName.Value },
            };

            await NavigationService.NavigateToAsync($"//{nameof(VerifyTwoFactorAuthenticationView)}", parameters);
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
            return UserName.Validate() && Password.Validate();
        }

        private void AddValidations()
        {
            UserName.Validations.Add(
                new IsNotNullOrEmptyRule<string>
                {
                    ValidationMessage = "El usuario es requerido."
                });

            Password.Validations.Add(
                new IsNotNullOrEmptyRule<string>
                {
                    ValidationMessage = "La contraseña es requerida."
                });
        }

        private void ClearValues()
        {
            _settingsService.AuthAccessToken = string.Empty;
            _settingsService.UserId = Guid.Empty;

            UserName.Reset();
            Password.Reset();

            IsValid = true;
            IsLogin = false;
        }
    }
}