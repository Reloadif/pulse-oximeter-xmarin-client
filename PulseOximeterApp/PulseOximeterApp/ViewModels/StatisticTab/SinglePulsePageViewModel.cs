using Microcharts;
using PulseOximeterApp.Data.DataBase;
using PulseOximeterApp.Models.HeartRate;
using PulseOximeterApp.Services;
using PulseOximeterApp.Services.DataBase;
using PulseOximeterApp.ViewModels.Base;
using System.Linq;

namespace PulseOximeterApp.ViewModels.StatisticTab
{
    public class SinglePulsePageViewModel : BaseViewModel
    {
        #region Fields
        private PulseStatistic _statistic;
        private LineChart _lineChart;
        private BaevskyIndicators _baevsky;
        #endregion

        #region Properties
        public PulseStatistic Statistic 
        {
            get => _statistic;
            set => Set(ref _statistic, value);
        }

        public LineChart MainChart
        {
            get => _lineChart;
            set => Set(ref _lineChart, value);
        }

        public BaevskyIndicators Baevsky
        {
            get => _baevsky;
            set => Set(ref _baevsky, value);
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

        public SinglePulsePageViewModel(PulseStatistic pulseStatistic)
        {
            Statistic = pulseStatistic;

            MainChart = new LineChart 
            {
                Entries = ConverterMeasurementPoints.From(pulseStatistic.MeasurementPoints).Select(mp => new ChartEntry(mp)
                {
                    Label = "ЧСС",
                    ValueLabel = mp.ToString(),
                    Color = ChartEntryColorConverter.FromPulse(mp),
                }).ToList(),
            };
            Baevsky = new BaevskyIndicators(Statistic.ABI, Statistic.VRI, Statistic.IARP, Statistic.VI);
        }
    }
}
