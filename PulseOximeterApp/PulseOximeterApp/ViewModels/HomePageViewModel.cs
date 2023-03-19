using PulseOximeterApp.Services.BluetoothLE;
using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.ViewModels.HomeTab;
using PulseOximeterApp.Views.HomeTab;
using System.Windows.Input;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels
{
    internal class HomePageViewModel : BaseViewModel
    {
        #region Fields
        private MicrocontrollerConnector _microcontrollerConnector;

        private bool _isActivityIndicator;
        #endregion

        #region Properties
        public bool IsActivityIndicator
        {
            get => _isActivityIndicator;
            set => Set(ref _isActivityIndicator, value);
        }
        #endregion

        #region Commands
        public ICommand ConnectToMicrocontroller { get; private set; }

        private async void ExecuteConnectToMicrocontroller(object obj)
        {
            IsActivityIndicator = true;

            if (await _microcontrollerConnector.Connect())
            {
                var Measure = new MeasurePage();
                var MeasureVM = new MeasurePageViewModel(_microcontrollerConnector);
                await Shell.Current.Navigation.PushAsync(Measure);
            }

            IsActivityIndicator = false;
        }
        #endregion

        public HomePageViewModel()
        {
            _microcontrollerConnector = new MicrocontrollerConnector();

            ConnectToMicrocontroller = new Command(ExecuteConnectToMicrocontroller);
        }
    }
}
