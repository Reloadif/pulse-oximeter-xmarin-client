using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.ViewModels.SettingTab;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PulseOximeterApp.Views.SettingTab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MemoryPage : ContentPage
    {
        public MemoryPage()
        {
            InitializeComponent();

            BindingContext = new MemoryPageViewModel();
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