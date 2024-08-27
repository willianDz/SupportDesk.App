using SupportDesk.App.Services.Settings;
using SupportDesk.App.Views;

namespace SupportDesk.App.Services.Navigation;

public class MauiNavigationService(ISettingsService settingsService) : INavigationAppService
{
    private readonly ISettingsService _settingsService = settingsService;

    public async Task InitializeAsync()
    {
        string uri = string.IsNullOrEmpty(_settingsService.AuthAccessToken)
            ? $"//{nameof(LoginView)}"
            : $"//Main/{nameof(HomeView)}";

        await NavigateToAsync(uri);
    }

    public async Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
    {
        var shellNavigation = new ShellNavigationState(route);

        if (routeParameters != null)
        {
            await Shell.Current.GoToAsync(shellNavigation, routeParameters);
        }
        else
        {
            await Shell.Current.GoToAsync(shellNavigation);
        }
    }

    public async Task PopAsync() =>
        await Shell.Current.GoToAsync("..");
}
