using PulseOximeterApp.Data.DataBase;
using PulseOximeterApp.Services.BluetoothLE;
using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.Views.HomeTab;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.HomeTab
{
    internal class MeasurementPageViewModel : BaseViewModel
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

        public MeasureSaturationPage MeasureSaturation { get; private set; }
        public MeasureSaturationPageViewModel MeasureSaturationVM { get; private set; }
        #endregion

        #region Commands
        public ICommand PulseMeasure { get; private set; }
        public ICommand SaturationMesure { get; private set; }

        private async void ExecutePulseMeasure(object obj)
        {
            IsActivityIndicator = true;

            if (MeasurePulseVM != null) MeasurePulseVM.Closing -= OnMeasurePulseClosing;

            MeasurePulse = new MeasurePulsePage();
            MeasurePulseVM = new MeasurePulsePageViewModel(_pulseOximeterService);
            MeasurePulseVM.Closing += OnMeasurePulseClosing;
            MeasurePulse.BindingContext = MeasurePulseVM;

            await Shell.Current.Navigation.PushAsync(MeasurePulse);
            IsActivityIndicator = false;
        }

        private async void ExecuteSaturationMesure(object obj)
        {
            IsActivityIndicator = true;

            if (MeasureSaturationVM != null) MeasureSaturationVM.Closing -= OnMeasureSaturationClosing;

            MeasureSaturation = new MeasureSaturationPage();
            MeasureSaturationVM = new MeasureSaturationPageViewModel(_pulseOximeterService);
            MeasureSaturationVM.Closing += OnMeasureSaturationClosing;
            MeasureSaturation.BindingContext = MeasureSaturationVM;

            await Shell.Current.Navigation.PushAsync(MeasureSaturation);
            IsActivityIndicator = false;
        }
        #endregion

        private async void OnMeasurePulseClosing(bool isSave)
        {
            if (isSave)
            {
                await App.StatisticDataBase.SavePulseStatisticAsync(new PulseStatistic()
                {
                    MeasurementPoints = ConverterMeasurementPoints.To(MeasurePulseVM.MainChart.Entries.Select(e => Convert.ToInt32(e.Value)).ToList()),
                    PointsCount = MeasurePulseVM.MainChart.Entries.Count(),
                    ABI = MeasurePulseVM.Baevsky.ABI,
                    VRI = MeasurePulseVM.Baevsky.VRI,
                    IARP = MeasurePulseVM.Baevsky.IARP,
                    VI = MeasurePulseVM.Baevsky.VI,
                });
            }

            await Shell.Current.Navigation.PopAsync();
        }

        private async void OnMeasureSaturationClosing(bool isSave)
        {
            if (isSave)
            {
                await App.StatisticDataBase.SaveSaturationStatisticAsync(new SaturationStatistic()
                {
                    MeasurementPoints = ConverterMeasurementPoints.To(MeasurePulseVM.MainChart.Entries.Select(e => Convert.ToInt32(e.Value)).ToList()),
                    PointsCount = MeasurePulseVM.MainChart.Entries.Count(),
                });
            }

            await Shell.Current.Navigation.PopAsync();
        }

        public MeasurementPageViewModel(PulseOximeterService pulseOximeterService)
        {
            _pulseOximeterService = pulseOximeterService;

            PulseMeasure = new Command(ExecutePulseMeasure);
            SaturationMesure = new Command(ExecuteSaturationMesure);
        }
    }
}
