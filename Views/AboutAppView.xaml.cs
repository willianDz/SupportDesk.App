using SupportDesk.App.Views.Base;

namespace SupportDesk.App.Views
{
    public partial class AboutAppView : ContentPageBase
    {
        public AboutAppView(AboutAppViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}