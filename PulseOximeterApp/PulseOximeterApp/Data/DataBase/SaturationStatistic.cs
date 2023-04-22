using SQLite;

namespace PulseOximeterApp.Data.DataBase
{
    public class SaturationStatistic
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string AddedDate { get; set; }

        public int PointsCount { get; set; }
        public int VeryLow { get; set; }
        public int Low { get; set; }
        public int Normal { get; set; }
    }
}
