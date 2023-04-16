using Microcharts;
using PulseOximeterApp.Services;
using PulseOximeterApp.Services.BluetoothLE;
using PulseOximeterApp.ViewModels.Base;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.HomeTab
{
    internal class MeasureSaturationPageViewModel : BaseViewModel
    {
        #region Fields
        private readonly ISaturationService _saturationService;
        private readonly IList<int> _saturationValues;
        private BarChart _mainChart;

        private readonly int _numberMeasure;
        private int _counterValue;
        private bool _isCompleteMeasure;

        private int _veryLowValues;
        private int _lowValues;
        private int _normalValues;
        #endregion

        #region Properties
        public BarChart MainChart
        {
            get => _mainChart;
            set => Set(ref _mainChart, value);
        }

        public int NumberMeasure
        {
            get => _numberMeasure;
        }

        public int CounterValue
        {
            get => _counterValue;
            set => Set(ref _counterValue, value);
        }

        public bool IsCompleteMeasure
        {
            get => _isCompleteMeasure;
            set => Set(ref _isCompleteMeasure, value);
        }

        public int VeryLowValues
        {
            get => _veryLowValues;
        }
        public int LowValues
        {
            get => _lowValues;
        }
        public int NormalValues
        {
            get => _normalValues;
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

        public ICommand SaveBack { get; private set; }

        private void ExecuteSaveBack(object obj)
        {
            Closing.Invoke(true);
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
            _saturationValues = new List<int>();
            _numberMeasure = _counterValue = Preferences.Get("NumberOfOxigenMeasure", 30);

            HeadBack = new Command(ExecuteHeadBack);
            SaveBack = new Command(ExecuteSaveBack);
        }

        #region Event Handler
        private void OnSaturationNotify(int value)
        {
            if (CounterValue > 0)
            {
                CounterValue -= 1;
                _saturationValues.Add(value);

                if (CounterValue == 0)
                {
                    _saturationService.StopMeasureSaturation();
                    MainChart = new BarChart()
                    {
                        LabelOrientation = Orientation.Horizontal,
                        ValueLabelOrientation = Orientation.Horizontal,
                        LabelTextSize = 30,
                        Entries = CalculateChartEntries(),
                    };
                    IsCompleteMeasure = true;
                }
            }
        }
        #endregion

        private IList<ChartEntry> CalculateChartEntries()
        {
            List<ChartEntry> result = new List<ChartEntry>();

            _veryLowValues = _saturationValues.Where(sv => sv < 90).Count();
            result.Add(new ChartEntry(_veryLowValues)
            {
                Label = "Очень низкий",
                ValueLabel = _veryLowValues.ToString(),
                Color = ChartEntryColor.SaturationVeryLow,
            });

            _lowValues = _saturationValues.Where(sv => 90 <= sv && sv < 95).Count();
            result.Add(new ChartEntry(_lowValues)
            {
                Label = "Низкий",
                ValueLabel = _lowValues.ToString(),
                Color = ChartEntryColor.SaturationLow,
            });

            _normalValues = _saturationValues.Where(sv => 95 <= sv && sv <= 100).Count();
            result.Add(new ChartEntry(_normalValues)
            {
                Label = "В норме",
                ValueLabel = _normalValues.ToString(),
                Color = ChartEntryColor.SaturationNormal,
            });

            return result;
        }
    }
}
