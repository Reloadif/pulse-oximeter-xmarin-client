using SQLite;
using System.Collections.Generic;

namespace PulseOximeterApp.Data.DataBase
{
    public class PulseStatistic
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

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

        public string MeasurementPoints { get; set; }
        public int PointsCount { get; set; }
    }

    public class ConverterMeasurementPoints
    {
        public static string To(IList<int> points)
        {
            string result = "";

            foreach (var point in points)
            {
                result += point.ToString() + ',';
            }

            return result.Remove(result.Length - 1);
        }

        public static IList<int> From(string points)
        {
            List<int> result = new List<int>();

            foreach (var point in points.Split(','))
            {
                result.Add(int.Parse(point));
            }

            return result;
        }
    }
}
