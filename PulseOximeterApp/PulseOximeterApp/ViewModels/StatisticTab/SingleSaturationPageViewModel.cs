using Microcharts;
using PulseOximeterApp.Data.DataBase;
using PulseOximeterApp.Services;
using PulseOximeterApp.ViewModels.Base;
using System.Linq;

namespace PulseOximeterApp.ViewModels.StatisticTab
{
    class SingleSaturationPageViewModel : BaseViewModel
    {
        #region Fields
        private SaturationStatistic _statistic;
        private LineChart _lineChart;
        #endregion

        #region Properties
        public SaturationStatistic Statistic
        {
            get => _statistic;
            set => Set(ref _statistic, value);
        }

        public LineChart MainChart
        {
            get => _lineChart;
            set => Set(ref _lineChart, value);
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

            MainChart = new LineChart
            {
                Entries = ConverterMeasurementPoints.From(saturationStatistic.MeasurementPoints).Select(mp => new ChartEntry(mp)
                {
                    Label = "Sp02",
                    ValueLabel = mp.ToString(),
                    Color = ChartEntryColorConverter.FromSaturation(mp),
                }).ToList(),
            };
        }
    }
}
