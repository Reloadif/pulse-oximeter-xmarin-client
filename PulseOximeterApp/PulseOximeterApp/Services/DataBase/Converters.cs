using System.Collections.Generic;

namespace PulseOximeterApp.Services.DataBase
{
    public class MeasurementPointsConverter
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
