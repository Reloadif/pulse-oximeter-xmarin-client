using Microcharts;
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
        private DonutChart _donutChart;

        private readonly int _numberMeasure;
        private int _counterValue;
        private bool _isCompleteMeasure;
        #endregion

        #region Properties
        public DonutChart MainChart
        {
            get => _donutChart;
            set => Set(ref _donutChart, value);
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
                    MainChart = new DonutChart()
                    {
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

            int L90 = _saturationValues.Where(sv => sv < 90).Count();
            result.Add(new ChartEntry(L90)
            {
                Label = "Очень низкий",
                ValueLabel = L90.ToString(),
                ValueLabelColor = SKColor.Parse("f24518"),
                Color = SKColor.Parse("f24518"),
            });

            int LE95 = _saturationValues.Where(sv => 90 <= sv && sv < 95).Count();
            result.Add(new ChartEntry(LE95)
            {
                Label = "Низкий",
                ValueLabel = LE95.ToString(),
                ValueLabelColor = SKColor.Parse("f1f518"),
                Color = SKColor.Parse("f1f518"),
            });

            int LE100 = _saturationValues.Where(sv => 95 <= sv && sv <= 100).Count();
            result.Add(new ChartEntry(LE100)
            {
                Label = "В норме",
                ValueLabel = LE100.ToString(),
                ValueLabelColor = SKColor.Parse("2bf518"),
                Color = SKColor.Parse("2bf518"),
            });

            return result;
        }
    }
}
