using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SupportDesk.App.Services.AppEnvironment;
using SupportDesk.App.Services.Dialog;
using SupportDesk.App.Services.Login;
using SupportDesk.App.Services.Navigation;
using SupportDesk.App.Services.NetworkService;
using SupportDesk.App.Services.RequestProvider;
using SupportDesk.App.Services.Settings;
using SupportDesk.App.Services.User;
using SupportDesk.App.Views;

namespace SupportDesk.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureEffects(
                effects =>
                {
                })
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fontawesome.ttf", "Fa-Icons");
            })
            .ConfigureEssentials(essentials =>
            {
                essentials.UseVersionTracking();
            })
            .RegisterAppServices()
            .RegisterViewModels()
            .RegisterViews();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ISettingsService, SettingsService>();
        mauiAppBuilder.Services.AddSingleton<INavigationAppService, MauiNavigationService>();
        mauiAppBuilder.Services.AddSingleton<IDialogAppService, DialogAppService>();
        mauiAppBuilder.Services.AddSingleton<IRequestProvider, RequestProvider>();
        mauiAppBuilder.Services.AddSingleton<ILoginService, LoginService>();
        mauiAppBuilder.Services.AddSingleton<INetworkService, NetworkService>();
        mauiAppBuilder.Services.AddSingleton<IPhoneDialer>(PhoneDialer.Default);
        mauiAppBuilder.Services.AddSingleton<IShare>(Share.Default);
        mauiAppBuilder.Services.AddSingleton<ILauncher>(Launcher.Default);
        mauiAppBuilder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        mauiAppBuilder.Services.AddSingleton<IAppEnvironmentService, AppEnvironmentService>(
            serviceProvider =>
            {
                var requestProvider = serviceProvider.GetService<IRequestProvider>();
                var settingsService = serviceProvider.GetService<ISettingsService>();

                var appEnviromentService = new AppEnvironmentService(
                    new UserMockService(), new UserService(requestProvider, settingsService)
                );

                appEnviromentService.UpdateDependencies(settingsService.UseMocks);
                return appEnviromentService;
            });

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<HomeViewModel>();
        mauiAppBuilder.Services.AddSingleton<AppShellViewModel>();
        mauiAppBuilder.Services.AddTransient<LoginViewModel>();
        mauiAppBuilder.Services.AddTransient<AboutAppViewModel>();
        mauiAppBuilder.Services.AddTransient<VerifyTwoFactorAuthenticationViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<LoginView>();
        mauiAppBuilder.Services.AddTransient<HomeView>();
        mauiAppBuilder.Services.AddTransient<AboutAppView>();
        mauiAppBuilder.Services.AddTransient<VerifyTwoFactorAuthenticationView>();

        return mauiAppBuilder;
    }
}
