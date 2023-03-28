using Plugin.BLE;
using Plugin.BLE.Abstractions.EventArgs;
using PulseOximeterApp.Services.BluetoothLE;
using PulseOximeterApp.Services.DependencyServices;
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
        private readonly MicrocontrollerConnector _microcontrollerConnector = new MicrocontrollerConnector();

        private bool _isActivityIndicator;

        private bool _isBluetoothOn;
        private bool _isLocationOn;
        #endregion

        #region Properties
        public bool IsActivityIndicator
        {
            get => _isActivityIndicator;
            set => Set(ref _isActivityIndicator, value);
        }

        public bool IsBluetoothOn
        {
            get => _isBluetoothOn;
            set
            {
                if (Set(ref _isBluetoothOn, value))
                {
                    (ConnectToMicrocontroller as Command).ChangeCanExecute();
                }
            }
        }
        public bool IsLocationOn
        {
            get => _isLocationOn;
            set
            {
                if (Set(ref _isLocationOn, value))
                {
                    (ConnectToMicrocontroller as Command).ChangeCanExecute();
                }
            }
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
                var MeasureVM = new MeasurePageViewModel(new PulseOximeterService(_microcontrollerConnector.GetDevice));
                Measure.BindingContext = MeasureVM;
                await Shell.Current.Navigation.PushAsync(Measure);
            }

            IsActivityIndicator = false;
        }
        private bool CanExecuteConnectToMicrocontroller(object obj)
        {
            return _isBluetoothOn && _isLocationOn;
        }
        #endregion

        #region Base Methods
        public override void OnAppearing()
        {
            base.OnAppearing();

            CrossBluetoothLE.Current.StateChanged += OnBluetoothLEStateChanged;
            DependencyService.Get<IGpsDependencyService>().GpsStatusChanged += OnLocationStateChanged;
            _microcontrollerConnector.ExceptionGenerated += OnExceptionMictrocontroller;
        }

        public override void OnDisappearing()
        {
            CrossBluetoothLE.Current.StateChanged -= OnBluetoothLEStateChanged;
            DependencyService.Get<IGpsDependencyService>().GpsStatusChanged -= OnLocationStateChanged;
            _microcontrollerConnector.ExceptionGenerated -= OnExceptionMictrocontroller;

            base.OnDisappearing();
        }
        #endregion

        public HomePageViewModel()
        {
            ConnectToMicrocontroller = new Command(ExecuteConnectToMicrocontroller, CanExecuteConnectToMicrocontroller);

            IsBluetoothOn = CrossBluetoothLE.Current.State == Plugin.BLE.Abstractions.Contracts.BluetoothState.On;
            IsLocationOn = DependencyService.Get<IGpsDependencyService>().IsGpsTurnedOn();
        }

        private void OnBluetoothLEStateChanged(object sender, BluetoothStateChangedArgs args)
        {
            IsBluetoothOn = args.NewState == Plugin.BLE.Abstractions.Contracts.BluetoothState.On;
        }
        private void OnLocationStateChanged(bool value)
        {
            IsLocationOn = value;
        }
        private async void OnExceptionMictrocontroller(string message)
        {
            await Shell.Current.DisplayAlert("Alert", message, "OK");
        }
    }
}
