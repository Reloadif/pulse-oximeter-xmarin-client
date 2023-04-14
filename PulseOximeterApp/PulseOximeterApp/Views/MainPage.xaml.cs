using PulseOximeterApp.ViewModels;
using PulseOximeterApp.Views;
using Xamarin.Forms;

namespace PulseOximeterApp
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(StatisticPage), typeof(StatisticPage));
            Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));

            BindingContext = new MainPageViewModel();
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            if (args.Source == ShellNavigationSource.PopToRoot) return;

            if (args.Source == ShellNavigationSource.ShellSectionChanged && StatisticTab.Navigation.NavigationStack.Count > 0)
            {
                StatisticTab.Navigation.PopToRootAsync();
            }
        }
    }
}
