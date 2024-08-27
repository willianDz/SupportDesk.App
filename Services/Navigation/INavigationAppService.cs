namespace SupportDesk.App.Services.Navigation;

public interface INavigationAppService
{
    Task InitializeAsync();

    Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null);

    Task PopAsync();
}
