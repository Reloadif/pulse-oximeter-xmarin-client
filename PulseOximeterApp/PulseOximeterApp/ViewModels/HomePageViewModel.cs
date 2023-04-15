using Plugin.BLE;
using Plugin.BLE.Abstractions.EventArgs;
using PulseOximeterApp.Infrastructure.DependencyServices;
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
                    OnPropertyChanged(nameof(BluetoothIcon));
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
                    OnPropertyChanged(nameof(LocationIcon));
                    (ConnectToMicrocontroller as Command).ChangeCanExecute();
                }
            }
        }

        public string BluetoothIcon
        {
            get => _isBluetoothOn ? "done" : "close";
        }

        public string LocationIcon
        {
            get => _isLocationOn ? "done" : "close";
        }
        #endregion

        #region Commands
        public ICommand ConnectToMicrocontroller { get; private set; }

        private async void ExecuteConnectToMicrocontroller(object obj)
        {
            IsActivityIndicator = true;

            if (await App.Microcontroller.Connect())
            {
                var Measurement = new MeasurementPage();
                var MeasurementVM = new MeasurementPageViewModel(new PulseOximeterService(App.Microcontroller.GetDevice));
                Measurement.BindingContext = MeasurementVM;
                await Shell.Current.Navigation.PushAsync(Measurement);
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
            App.Microcontroller.ExceptionGenerated += OnExceptionGenerated;
        }

        public override void OnDisappearing()
        {
            CrossBluetoothLE.Current.StateChanged -= OnBluetoothLEStateChanged;
            DependencyService.Get<IGpsDependencyService>().GpsStatusChanged -= OnLocationStateChanged;
            App.Microcontroller.ExceptionGenerated -= OnExceptionGenerated;

            base.OnDisappearing();
        }
        #endregion

        public HomePageViewModel()
        {
            ConnectToMicrocontroller = new Command(ExecuteConnectToMicrocontroller, CanExecuteConnectToMicrocontroller);

            IsBluetoothOn = CrossBluetoothLE.Current.State == Plugin.BLE.Abstractions.Contracts.BluetoothState.On;
            IsLocationOn = DependencyService.Get<IGpsDependencyService>().IsGpsTurnedOn();
        }

        #region Event Handlers
        private void OnBluetoothLEStateChanged(object sender, BluetoothStateChangedArgs args)
        {
            IsBluetoothOn = args.NewState == Plugin.BLE.Abstractions.Contracts.BluetoothState.On;
        }
        private void OnLocationStateChanged(bool value)
        {
            IsLocationOn = value;
        }
        private async void OnExceptionGenerated(string message)
        {
            await DependencyService.Get<IShowMessageDependencyService>().ShowAlertAsync(message);
        }
        #endregion
    }
}
