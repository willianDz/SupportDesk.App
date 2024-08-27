using SupportDesk.App.Services.Navigation;
using SupportDesk.App.Views;

namespace SupportDesk.App
{
    public partial class AppShell : Shell
    {
        private readonly INavigationAppService _navigationService;

        public AppShell(
            INavigationAppService navigationService,
            AppShellViewModel viewModel
            )
        {
            _navigationService = navigationService;

            InitializeRouting();

            InitializeComponent();

            BindingContext = viewModel;
        }

        protected override async void OnHandlerChanged()
        {
            base.OnHandlerChanged();

            if (Handler is not null)
            {
                await _navigationService.InitializeAsync();
            }
        }

        private static void InitializeRouting()
        {
            Routing.RegisterRoute(nameof(AboutAppView), typeof(AboutAppView));
        }
    }
}
