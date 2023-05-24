using PulseOximeterApp.ViewModels.Base;
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
            await Shell.Current.Navigation.PushAsync(new OptionPage());
        }

        public ICommand GoMemoryPage { get; private set; }

        private async void ExecuteGoMemoryPage(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new MemoryPage());
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
            GoMemoryPage = new Command(ExecuteGoMemoryPage);
        }
    }
}
