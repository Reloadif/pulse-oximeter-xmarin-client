using PulseOximeterApp.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp.Views.StatisticTab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleSaturationPage : ContentPage
    {
        public SingleSaturationPage()
        {
            InitializeComponent();
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