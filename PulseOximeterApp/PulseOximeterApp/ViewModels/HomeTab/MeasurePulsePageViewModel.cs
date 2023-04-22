using Microcharts;
using PulseOximeterApp.Models;
using PulseOximeterApp.Models.HeartRate;
using PulseOximeterApp.Services;
using PulseOximeterApp.Services.BluetoothLE;
using PulseOximeterApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.HomeTab
{
    internal class MeasurePulsePageViewModel : BaseViewModel
    {
        #region Fields
        private readonly IPulseService _pulseService;
        private readonly IList<double> _cardioIntervals;
        private LineChart _lineChart;

        private readonly int _numberMeasure;
        private int _counterValue;
        private bool _isCompleteMeasure;

        private BaevskyIndicators _baevskyIndicators;
        private PulseCommonInformation _commonInformation;
        #endregion

        #region Properties
        public LineChart MainChart
        {
            get => _lineChart;
            set => Set(ref _lineChart, value);
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

        public BaevskyIndicators Baevsky
        {
            get => _baevskyIndicators;
            set => Set(ref _baevskyIndicators, value);
        }

        public PulseCommonInformation CommonInformation
        {
            get => _commonInformation;
            set => Set(ref _commonInformation, value);
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

            _pulseService.PulseNotify += OnPulseNotify;
            _pulseService.StartMeasurePulse();
        }

        public override void OnDisappearing()
        {
            if (!IsCompleteMeasure) _pulseService.StopMeasurePulse();
            _pulseService.PulseNotify -= OnPulseNotify;

            (Application.Current.MainPage.BindingContext as MainPageViewModel).StatisticTabIsEnabled = true;
            (Application.Current.MainPage.BindingContext as MainPageViewModel).SettingTabIsEnabled = true;

            base.OnDisappearing();
        }
        #endregion

        public MeasurePulsePageViewModel(IPulseService pulseService)
        {
            _pulseService = pulseService;

            _cardioIntervals = new List<double>();
            _numberMeasure = _counterValue = Preferences.Get("NumberOfPulseMeasure", 30);

            HeadBack = new Command(ExecuteHeadBack);
            SaveBack = new Command(ExecuteSaveBack);
        }

        #region Event Handler
        private void OnPulseNotify(int value)
        {
            if (CounterValue > 0)
            {
                CounterValue -= 1;
                _cardioIntervals.Add(value / 1000d);

                if (CounterValue == 0)
                {
                    _pulseService.StopMeasurePulse();
                    MainChart = new LineChart()
                    {
                        Entries = CalculateChartEntries(),
                    };

                    Baevsky = new BaevskyIndicators(new HeartRateVariability(_cardioIntervals));
                    CommonInformation = new PulseCommonInformation(MainChart.Entries.Select(e => Convert.ToInt32(e.Value)).ToList());
                    IsCompleteMeasure = true;
                }
            }
        }
        #endregion

        private IList<ChartEntry> CalculateChartEntries()
        {
            List<int> interim = _cardioIntervals.Select(ci => Convert.ToInt32(60 / ci)).ToList();
            List<int> result = new List<int>();

            int elementsInBatch = interim.Count / 30;
            for (int i = 0; i < 30; ++i)
            {
                result.Add(CalculateAverageBPM(interim, i, elementsInBatch));
            }

            return result.Select(v => new ChartEntry(v)
            {
                Label = "ЧСС",
                ValueLabel = v.ToString(),
                Color = ChartEntryToSKColorConverter.FromPulse(v),
            }).ToList();
        }

        private int CalculateAverageBPM(List<int> values, int currentV, int valuesInBatch) 
        {
            int result;
            valuesInBatch = valuesInBatch >= 4 ? valuesInBatch : 4;

            if (currentV + valuesInBatch <= values.Count - 1)
            {
                result = values.GetRange(currentV, valuesInBatch).Sum() / valuesInBatch;
            }
            else
            {
                result = values.GetRange(currentV - valuesInBatch, valuesInBatch).Sum() / valuesInBatch;
            }

            return result;
        }
    }
}
