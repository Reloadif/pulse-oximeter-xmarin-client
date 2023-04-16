using Microcharts;
using PulseOximeterApp.Data.DataBase;
using PulseOximeterApp.Services;
using PulseOximeterApp.ViewModels.Base;
using System.Collections.Generic;

namespace PulseOximeterApp.ViewModels.StatisticTab
{
    class SingleSaturationPageViewModel : BaseViewModel
    {
        #region Fields
        private SaturationStatistic _statistic;
        private BarChart _mainChart;
        #endregion

        #region Properties
        public SaturationStatistic Statistic
        {
            get => _statistic;
            set => Set(ref _statistic, value);
        }

        public BarChart MainChart
        {
            get => _mainChart;
            set => Set(ref _mainChart, value);
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

            MainChart = new BarChart()
            {
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
                LabelTextSize = 30,
                Entries = new List<ChartEntry>()
                {
                    new ChartEntry(Statistic.VeryLow)
                    {
                        Label = "Очень низкий",
                        ValueLabel = Statistic.VeryLow.ToString(),
                        Color = ChartEntryColor.SaturationVeryLow,
                    },
                    new ChartEntry(Statistic.Low)
                    {
                        Label = "Низкий",
                        ValueLabel = Statistic.Low.ToString(),
                        Color = ChartEntryColor.SaturationLow,
                    },
                    new ChartEntry(Statistic.Normal)
                    {
                        Label = "В норме",
                        ValueLabel = Statistic.Normal.ToString(),
                        Color = ChartEntryColor.SaturationNormal,
                    },
                }
            };
        }
    }
}
