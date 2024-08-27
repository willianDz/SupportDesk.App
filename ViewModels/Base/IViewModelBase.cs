using SupportDesk.App.Services.Dialog;
using SupportDesk.App.Services.Navigation;
using SupportDesk.App.Services.Settings;

namespace SupportDesk.App.ViewModels.Base;

public interface IViewModelBase : IQueryAttributable
{
    public IDialogAppService DialogService { get; }
    public INavigationAppService NavigationService { get; }
    public ISettingsService SettingsService { get; }

    public bool IsBusy { get; }
    public bool Initializing { get; set; }
    public string InitializingLoadingText { get; set; }
    public bool IsInitialized { get; set; }
    public bool DataLoaded { get; set; }

    public bool IsErrorState { get; set; }
    public string ErrorMessage { get; set; }
    public string ErrorImage { get; set; }

    Task InitializeAsync();
    Task OnAppearingAsync();
    Task OnDisappearingAsync();
}
