using Microcharts;
using PulseOximeterApp.Services.BluetoothLE;
using PulseOximeterApp.ViewModels.Base;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.HomeTab
{
    internal class MeasureSaturationPageViewModel : BaseViewModel
    {
        #region Fields
        private readonly ISaturationService _saturationService;
        private readonly IList<ChartEntry> _chartEntries;
        private LineChart _lineChart;

        private readonly int _numberOfMeasure;
        private int _valueOfCounter;
        private bool _isCompleteMeasure;
        #endregion

        #region Properties
        public LineChart MainChart
        {
            get => _lineChart;
            set => Set(ref _lineChart, value);
        }

        public int CounterValue
        {
            get => _valueOfCounter;
            set => Set(ref _valueOfCounter, value);
        }

        public bool IsCompleteMeasure
        {
            get => _isCompleteMeasure;
            set => Set(ref _isCompleteMeasure, value);
        }
        #endregion

        #region Events
        public event Action<bool> Closing;
        #endregion

        #region Commands
        public ICommand HeadBack { get; private set; }

        private void ExecuteHeadBack(object obj)
        {
            Closing.Invoke(false);
        }
        #endregion

        #region Base Methods
        public override void OnAppearing()
        {
            base.OnAppearing();

            (Application.Current.MainPage.BindingContext as MainPageViewModel).StatisticTabIsEnabled = false;
            (Application.Current.MainPage.BindingContext as MainPageViewModel).SettingTabIsEnabled = false;

            _saturationService.SaturationNotify += OnSaturationNotify;
            _saturationService.StartMeasureSaturation();
        }

        public override void OnDisappearing()
        {
            if (!IsCompleteMeasure) _saturationService.StopMeasureSaturation();
            _saturationService.SaturationNotify -= OnSaturationNotify;

            (Application.Current.MainPage.BindingContext as MainPageViewModel).StatisticTabIsEnabled = true;
            (Application.Current.MainPage.BindingContext as MainPageViewModel).SettingTabIsEnabled = true;

            base.OnDisappearing();
        }
        #endregion

        public MeasureSaturationPageViewModel(ISaturationService saturationService)
        {
            _saturationService = saturationService;
            _chartEntries = new List<ChartEntry>();
            _numberOfMeasure = Preferences.Get("NumberOfOxigenMeasure", 30);
            _valueOfCounter = _numberOfMeasure;

            HeadBack = new Command(ExecuteHeadBack);
        }

        private void OnSaturationNotify(int value)
        {
            if (CounterValue > 0)
            {
                CounterValue -= 1;
                _chartEntries.Add(new ChartEntry(value)
                {
                    Label = "Sp02",
                    ValueLabel = value.ToString(),
                    Color = CalculateColorForChartEnty(value),
                });

                if (CounterValue == 0)
                {
                    _saturationService.StopMeasureSaturation();
                    MainChart = new LineChart()
                    {
                        Entries = _chartEntries
                    };
                    IsCompleteMeasure = true;
                }
            }
        }

        private SKColor CalculateColorForChartEnty(int value)
        {
            SKColor result = SKColor.Parse("f24518");

            if (value < 90) result = SKColor.Parse("f24518");
            else if (value < 95) result = SKColor.Parse("f1f518");
            else if (value <= 100) result = SKColor.Parse("2bf518");

            return result;
        }
    }
}
