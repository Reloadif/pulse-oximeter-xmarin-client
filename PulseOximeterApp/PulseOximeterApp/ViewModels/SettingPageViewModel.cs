using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.ViewModels.SettingTab;
using PulseOximeterApp.Views.SettingTab;
using System.Windows.Input;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels
{
    class SettingPageViewModel : BaseViewModel
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Commands
        public ICommand GoOptionPage { get; private set; }

        private async void ExecuteGoOptionPage(object obj)
        {
            var Option = new OptionPage();
            var OptionVM = new OptionPageViewModel();
            Option.BindingContext = OptionVM;
            await Shell.Current.Navigation.PushAsync(Option);
        }
        #endregion

        #region Base Methods
        public override void OnAppearing()
        {
            base.OnAppearing();
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        #endregion

        public SettingPageViewModel()
        {
            GoOptionPage = new Command(ExecuteGoOptionPage);
        }
    }
}
