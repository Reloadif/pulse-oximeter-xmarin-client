using Microcharts;
using PulseOximeterApp.Data.DataBase;
using PulseOximeterApp.Models;
using PulseOximeterApp.Models.CommonInformation;
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

        private SaturationCommonInformation _commonInformation;
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

        public SaturationCommonInformation CommonInformation
        {
            get => _commonInformation;
            set => Set(ref _commonInformation, value);
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
                    new ChartEntry(Statistic.VeryBad)
                    {
                        Label = "Очень низкий",
                        ValueLabel = Statistic.VeryBad.ToString(),
                        Color = ChartEntryColor.SaturationVeryLow,
                    },
                    new ChartEntry(Statistic.Bad)
                    {
                        Label = "Низкий",
                        ValueLabel = Statistic.Bad.ToString(),
                        Color = ChartEntryColor.SaturationLow,
                    },
                    new ChartEntry(Statistic.Good)
                    {
                        Label = "В норме",
                        ValueLabel = Statistic.Good.ToString(),
                        Color = ChartEntryColor.SaturationNormal,
                    },
                }
            };

            CommonInformation = new SaturationCommonInformation(Statistic.CommonInformationRecord);
        }
    }
}
