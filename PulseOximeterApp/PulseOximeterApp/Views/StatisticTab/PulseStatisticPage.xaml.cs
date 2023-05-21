using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.ViewModels.StatisticTab;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp.Views.StatisticTab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PulseStatisticPage : ContentPage
    {
        public PulseStatisticPage()
        {
            InitializeComponent();

            BindingContext = new PulseStatisticPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as BaseViewModel)?.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (BindingContext as BaseViewModel)?.OnDisappearing();
        }
    }
}