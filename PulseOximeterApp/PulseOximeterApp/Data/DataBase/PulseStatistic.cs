using SQLite;
using SQLiteNetExtensions.Attributes;

namespace PulseOximeterApp.Data.DataBase
{
    public class PulseStatistic
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string AddedDate { get; set; }

        public string MeasurementPoints { get; set; }
        public int PointsCount { get; set; }

        [OneToOne]
        public PulseCommonInformationRecord CommonRecord { get; set; }
        [OneToOne]
        public BaevskyIndicatorsRecord BaevskyRecord { get; set; }
    }

    public class PulseCommonInformationRecord
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(PulseStatistic))]
        public int PulseStatisticId { get; set; }

        public StatisticStatus Status { get; set; }
        public int AverageBPM { get; set; }
        public int NormalPulseMeasurement { get; set; }
        public string Recommendation { get; set; }
    }

    public class BaevskyIndicatorsRecord
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(PulseStatistic))]
        public int PulseStatisticId { get; set; }

        public double ABI { get; set; }
        public double VRI { get; set; }
        public double IARP { get; set; }
        public double VI { get; set; }
    }
}
