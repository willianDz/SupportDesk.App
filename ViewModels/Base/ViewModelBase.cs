using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SupportDesk.App.Services.Dialog;
using SupportDesk.App.Services.Navigation;
using SupportDesk.App.Services.Settings;

namespace SupportDesk.App.ViewModels.Base;

public abstract partial class ViewModelBase : ObservableObject, IViewModelBase, IDisposable
{
    private readonly SemaphoreSlim _isBusyLock = new(1, 1);

    private bool _disposedValue;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private bool initializing;

    [ObservableProperty]
    private string initializingLoadingText;

    [ObservableProperty]
    private bool isInitialized;

    [ObservableProperty]
    private bool dataLoaded;

    [ObservableProperty]
    private bool isErrorState;

    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private string errorImage;

    [RelayCommand]
    async Task RetryInitialize()
    {
        ClearErrorState();

        await InitializeAsync();
    }

    public IDialogAppService DialogService { get; private set; }

    public INavigationAppService NavigationService { get; private set; }

    public ISettingsService SettingsService { get; private set; }

    protected ViewModelBase(
        IDialogAppService dialogService,
        INavigationAppService navigationService,
        ISettingsService settingsService
    )
    {
        DialogService = dialogService;
        NavigationService = navigationService;
        SettingsService = settingsService;
    }

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
    }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task OnAppearingAsync()
    {

        return Task.CompletedTask;
    }

    public virtual Task OnDisappearingAsync()
    {
        return Task.CompletedTask;
    }

    public async Task IsBusyFor(Func<Task> unitOfWork)
    {
        await _isBusyLock.WaitAsync();

        try
        {
            IsBusy = true;

            await unitOfWork();
        }
        finally
        {
            IsBusy = false;
            _isBusyLock.Release();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _isBusyLock?.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async Task ShowToast(
        string message,
        ToastDuration duration = ToastDuration.Long,
        double textSize = 12)
    {
        var toast = Toast.Make(message, duration, textSize);

        await toast.Show();
    }

    public void SetInitializingIndicators(bool isStaring = true, string loadingText = "")
    {
        if (isStaring)
        {
            Initializing = true;
            InitializingLoadingText = loadingText;
            DataLoaded = false;
            ClearErrorState();
        }
        else
        {
            InitializingLoadingText = string.Empty;
            Initializing = false;
        }
    }

    public void ErrorNoInternet()
    {
        SetErrorState(
            "No hay conexión a internet. Por favor revise su conexión.",
            "nointernet.png");
    }

    public void SetErrorState(
        string errorMessage = "Ocurrió un error al cargar la información.",
        string errorImage = "error.png")
    {
        IsErrorState = true;
        ErrorMessage = errorMessage;
        ErrorImage = errorImage;
    }

    public void ClearErrorState()
    {
        IsErrorState = false;
        ErrorMessage = string.Empty;
        ErrorImage = string.Empty;
    }
}
