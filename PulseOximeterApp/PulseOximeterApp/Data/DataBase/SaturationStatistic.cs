using SQLite;
using SQLiteNetExtensions.Attributes;

namespace PulseOximeterApp.Data.DataBase
{
    public class SaturationStatistic
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string AddedDate { get; set; }

        public int PointsCount { get; set; }
        public int VeryBad { get; set; }
        public int Bad { get; set; }
        public int Good { get; set; }

        [OneToOne]
        public SaturationCommonInformationRecord CommonInformationRecord { get; set; }
    }

    public class SaturationCommonInformationRecord
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(SaturationStatistic))]
        public int SaturationStatisticId { get; set; }

        public StatisticStatus Status { get; set; }
        public int NormalPulseMeasurement { get; set; }
        public string Recommendation { get; set; }
    }
}
