using Microcharts;
using PulseOximeterApp.Services.BluetoothLE;
using PulseOximeterApp.ViewModels.Base;
using PulseOximeterApp.Views.HomeTab;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace PulseOximeterApp.ViewModels.HomeTab
{
    internal class MeasurePulsePageViewModel : BaseViewModel
    {
        #region Fields
        private readonly IPulseService _pulseService;
        private readonly IList<ChartEntry> _chartEntries;
        private LineChart _lineChart;

        private CancellationTokenSource _cancellationTokenSource;

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
        public event Action<bool> OnClosing;
        #endregion

        #region Commands
        public ICommand HeadBack { get; private set; }

        private void ExecuteHeadBack(object obj)
        {
            OnClosing.Invoke(false);
        }
        #endregion

        public MeasurePulsePageViewModel(IPulseService pulseService)
        {
            _pulseService = pulseService;
            _chartEntries = new List<ChartEntry>();
            _cancellationTokenSource = new CancellationTokenSource();

            _numberOfMeasure = 30;
            _valueOfCounter = _numberOfMeasure;

            HeadBack = new Command(ExecuteHeadBack);

            PreparePulseService();
        }

        private void PreparePulseService()
        {
            _pulseService.OnPulseNotify += (int value) =>
            {
                if (!_isCompleteMeasure)
                {
                    CounterValue -= 1;

                    _chartEntries.Add(new ChartEntry(value)
                    {
                        Label = "BPM",
                        ValueLabel = value.ToString(),
                        Color = CalculateColorForChartEnty(value),
                    });

                    if (_chartEntries.Count > _numberOfMeasure)
                    {
                        _cancellationTokenSource.Cancel();
                        IsCompleteMeasure = true;
                        MainChart = new LineChart()
                        {
                            Entries = _chartEntries
                        };
                    }
                }
            };

            _pulseService.StartMeasurePulse(_cancellationTokenSource.Token);
        }

        private SKColor CalculateColorForChartEnty(int value)
        {
            if (value < 45) return SKColor.Parse("f24518");
            else if (value < 80) return SKColor.Parse("2bf518");
            else if (value < 100) return SKColor.Parse("f1f518");
            else return SKColor.Parse("f24518");
        }
    }
}
