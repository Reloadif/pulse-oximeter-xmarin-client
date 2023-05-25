using Plugin.BLE.Abstractions.Contracts;
using PulseOximeterApp.Data.DataBase;
using PulseOximeterApp.Services.BluetoothLE.Services;
using PulseOximeterApp.Services.DataBase;
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

        private readonly IDevice _connectedDevice;
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
            MeasurePulseVM = new MeasurePulsePageViewModel(new PulseService(_connectedDevice));
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
            MeasureSaturationVM = new MeasureSaturationPageViewModel(new SaturationService(_connectedDevice));
            MeasureSaturationVM.Closing += OnMeasureSaturationClosing;
            MeasureSaturation.BindingContext = MeasureSaturationVM;

            await Shell.Current.Navigation.PushAsync(MeasureSaturation);
            IsActivityIndicator = false;
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

        public MeasurementPageViewModel(IDevice connectedDevice)
        {
            _connectedDevice = connectedDevice;

            PulseMeasure = new Command(ExecutePulseMeasure);
            SaturationMesure = new Command(ExecuteSaturationMesure);
        }

        #region Event Handler
        private async void OnMeasurePulseClosing(bool isSave)
        {
            if (isSave)
            {
                var pulseStatistic = new PulseStatistic()
                {
                    AddedDate = DateTime.Now.ToString(),
                    MeasurementPoints = MeasurementPointsConverter.To(MeasurePulseVM.MainChart.Entries.Select(e => Convert.ToInt32(e.Value)).ToList()),
                    PointsCount = MeasurePulseVM.NumberMeasure,
                };
                await App.StatisticDataBase.InsertPulseStatisticAsync(pulseStatistic);

                var commonInformationRecord = new PulseCommonInformationRecord()
                {
                    Status = MeasurePulseVM.CommonInformation.Status,
                    AverageBPM = MeasurePulseVM.CommonInformation.AverageBPM,
                    NormalPulseMeasurement = MeasurePulseVM.CommonInformation.NormalPulseMeasurement,
                    Recommendation = MeasurePulseVM.CommonInformation.Recommendation,
                };
                await App.StatisticDataBase.SavePulseCommonInformationRecordAsync(commonInformationRecord);

                var baevskyRecord = new BaevskyIndicatorsRecord()
                {
                    ABI = MeasurePulseVM.Baevsky.ABI,
                    VRI = MeasurePulseVM.Baevsky.VRI,
                    IARP = MeasurePulseVM.Baevsky.IARP,
                    VI = MeasurePulseVM.Baevsky.VI,
                };
                await App.StatisticDataBase.SaveBaevskyIndicatorsRecordAsync(baevskyRecord);

                pulseStatistic.CommonInformationRecord = commonInformationRecord;
                pulseStatistic.BaevskyRecord = baevskyRecord;

                await App.StatisticDataBase.UpdatePulseStatisticAsync(pulseStatistic);
            }

            await Shell.Current.Navigation.PopAsync();
        }

        private async void OnMeasureSaturationClosing(bool isSave)
        {
            if (isSave)
            {
                var saturationStatistic = new SaturationStatistic()
                {
                    AddedDate = DateTime.Now.ToString(),
                    PointsCount = MeasureSaturationVM.NumberMeasure,
                    VeryBad = MeasureSaturationVM.VeryLowValues,
                    Bad = MeasureSaturationVM.LowValues,
                    Good = MeasureSaturationVM.NormalValues,
                };

                await App.StatisticDataBase.InsertSaturationStatisticsync(saturationStatistic);

                var commonInformationRecord = new SaturationCommonInformationRecord()
                {
                    Status = MeasureSaturationVM.CommonInformation.Status,
                    NormalPulseMeasurement = MeasureSaturationVM.CommonInformation.NormalPulseMeasurement,
                    Recommendation = MeasureSaturationVM.CommonInformation.Recommendation,
                };
                await App.StatisticDataBase.SaveSaturationCommonInformationRecordAsync(commonInformationRecord);

                saturationStatistic .CommonInformationRecord = commonInformationRecord;
                await App.StatisticDataBase.UpdateSaturationStatisticAsync(saturationStatistic);
            }

            await Shell.Current.Navigation.PopAsync();
        }
        #endregion
    }
}
