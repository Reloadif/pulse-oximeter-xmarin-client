﻿using PulseOximeterApp.ViewModels;
using PulseOximeterApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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

            if (args.Source != ShellNavigationSource.ShellSectionChanged) return;

            if (StatisticTab.Navigation.NavigationStack.Count > 1)
            {
                StatisticTab.Navigation.PopToRootAsync();
            }

            if (SettingTab.Navigation.NavigationStack.Count > 1)
            {
                SettingTab.Navigation.PopToRootAsync();
            }
        }
    }
}
