using SQLite;
using System.Collections.Generic;

namespace PulseOximeterApp.Data.DataBase
{
    public class PulseStatistic
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string AddedDate { get; set; }

        public string MeasurementPoints { get; set; }
        public int PointsCount { get; set; }

        public double ABI { get; set; }
        public double VRI { get; set; }
        public double IARP { get; set; }
        public double VI { get; set; }
    }

    public class SaturationStatistic
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string AddedDate { get; set; }

        public string MeasurementPoints { get; set; }
        public int PointsCount { get; set; }
    }
}
