using PulseOximeterApp.Services.BluetoothLE;
using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.Views.HomeTab;
using System.Windows.Input;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.HomeTab
{
    internal class MeasurePageViewModel : BaseViewModel
    {
        #region Fields
        private bool _isActivityIndicator;

        private readonly PulseOximeterService _pulseOximeterService;
        #endregion

        #region Properties
        public bool IsActivityIndicator
        {
            get => _isActivityIndicator;
            set => Set(ref _isActivityIndicator, value);
        }

        public MeasurePulsePage MeasurePulse { get; private set; }
        public MeasurePulsePageViewModel MeasurePulseVM { get; private set; }
        #endregion

        #region Commands
        public ICommand PulseMeasure { get; private set; }
        public ICommand SaturationMesure { get; private set; }

        private async void ExecutePulseMeasure(object obj)
        {
            (Application.Current.MainPage.BindingContext as MainPageViewModel).StatisticTabIsEnabled = false;
            (Application.Current.MainPage.BindingContext as MainPageViewModel).SettingTabIsEnabled = false;

            IsActivityIndicator = true;
            await BeginInvokeOnMainThreadAsync<object>(() =>
            {
                MeasurePulse = new MeasurePulsePage();
                MeasurePulseVM = new MeasurePulsePageViewModel(_pulseOximeterService);
                MeasurePulseVM.OnClosing += OnMeasurePulseClosing;
                MeasurePulse.BindingContext = MeasurePulseVM;

                return null;
            });

            await Shell.Current.Navigation.PushAsync(MeasurePulse);
            IsActivityIndicator = false;
        }
        #endregion

        private async void OnMeasurePulseClosing(bool isSave)
        {
            await Shell.Current.Navigation.PopAsync();

            (Application.Current.MainPage.BindingContext as MainPageViewModel).StatisticTabIsEnabled = true;
            (Application.Current.MainPage.BindingContext as MainPageViewModel).SettingTabIsEnabled = true;
        }

        public MeasurePageViewModel(PulseOximeterService pulseOximeterService)
        {
            _pulseOximeterService = pulseOximeterService;

            PulseMeasure = new Command(ExecutePulseMeasure);
        }
    }
}
