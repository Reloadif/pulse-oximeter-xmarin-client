using Microcharts;
using PulseOximeterApp.Data.DataBase;
using PulseOximeterApp.Models;
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

        private PulseCommonInformation _commonInformation;
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

        public PulseCommonInformation CommonInformation
        {
            get => _commonInformation;
            set => Set(ref _commonInformation, value);
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
                Entries = MeasurementPointsConverter.From(pulseStatistic.MeasurementPoints).Select(mp => new ChartEntry(mp)
                {
                    Label = "ЧСС",
                    ValueLabel = mp.ToString(),
                    Color = ChartEntryToSKColorConverter.FromPulse(mp),
                }).ToList(),
            };

            CommonInformation = new PulseCommonInformation(pulseStatistic.CommonInformationRecord);
            Baevsky = new BaevskyIndicators(Statistic.BaevskyRecord);
        }
    }
}
