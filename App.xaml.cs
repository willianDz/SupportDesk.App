using SupportDesk.App.Services.AppEnvironment;
using SupportDesk.App.Services.Navigation;
using SupportDesk.App.Services.Settings;

namespace SupportDesk.App;

public partial class App : Application
{
    private readonly ISettingsService _settingsService;
    private readonly IAppEnvironmentService _appEnvironmentService;

    public App(
        ISettingsService settingsService,
        IAppEnvironmentService appEnvironmentService,
        INavigationAppService navigationService,
        AppShellViewModel appShellViewModel)
    {
        _settingsService = settingsService;
        _appEnvironmentService = appEnvironmentService;

        InitializeComponent();
        InitApp();
        MainPage = new AppShell(navigationService, appShellViewModel);
    }

    private void InitApp()
    {
        if (!_settingsService.UseMocks)
        {
            _appEnvironmentService.UpdateDependencies(_settingsService.UseMocks);
        }
    }
}
