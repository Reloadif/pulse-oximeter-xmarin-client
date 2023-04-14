using Microcharts;
using PulseOximeterApp.Data.DataBase;
using PulseOximeterApp.ViewModels.Base;
using SkiaSharp;
using System.Linq;
using static SQLite.SQLite3;

namespace PulseOximeterApp.ViewModels.StatisticTab
{
    class SingleSaturationPageViewModel : BaseViewModel
    {
        #region Fields
        private SaturationStatistic _statistic;
        private DonutChart _donutChart;

        private int _colorCounter;
        #endregion

        #region Properties
        public SaturationStatistic Statistic
        {
            get => _statistic;
            set => Set(ref _statistic, value);
        }

        public DonutChart MainChart
        {
            get => _donutChart;
            set => Set(ref _donutChart, value);
        }
        #endregion

        #region Commands
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

        public SingleSaturationPageViewModel(SaturationStatistic saturationStatistic)
        {
            Statistic = saturationStatistic;
            _colorCounter = 0;

            MainChart = new DonutChart
            {
                Entries = ConverterMeasurementPoints.From(saturationStatistic.MeasurementPoints).Select(mp => new ChartEntry(mp)
                {
                    Label = GetLabel(),
                    ValueLabel = mp.ToString(),
                    Color = GetColor(),
                }).ToList(),
            };
        }

        private string GetLabel()
        {
            string result = "";

            if (_colorCounter == 0)
            {
                result = "< 90%";
            }
            else if (_colorCounter == 1)
            {
                result = "90% <=...< 95%";
            }
            else if (_colorCounter == 2)
            {
                result = "95% <=...<= 100%";
            }

            return result;
        }

        private SKColor GetColor()
        {
            SKColor result = SKColor.Parse("f24518");

            if (_colorCounter == 0) result = SKColor.Parse("f24518");
            else if (_colorCounter == 1) result = SKColor.Parse("f1f518");
            else if (_colorCounter == 2) result = SKColor.Parse("2bf518");

            _colorCounter += 1;
            return result;
        }
    }
}
