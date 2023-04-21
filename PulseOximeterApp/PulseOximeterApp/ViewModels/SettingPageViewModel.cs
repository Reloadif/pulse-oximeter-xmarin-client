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
            var option = new OptionPage();
            var optionVM = new OptionPageViewModel();
            option.BindingContext = optionVM;
            await Shell.Current.Navigation.PushAsync(option);
        }

        public ICommand GoMemoryPage { get; private set; }

        private async void ExecuteGoMemoryPage(object obj)
        {
            var memory = new MemoryPage();
            var memoryVM = new MemoryPageViewModel();
            memory.BindingContext = memoryVM;
            await Shell.Current.Navigation.PushAsync(memory);
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
