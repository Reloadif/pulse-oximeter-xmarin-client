using PulseOximeterApp.Data.DataBase;
using Xamarin.Forms;

namespace PulseOximeterApp.Services.Colors
{
    public class StatisticStatusColor
    {
        public static readonly Color Default = Color.FromHex("#f24518");
        public static readonly Color VeryBad = Color.FromHex("#f24518");
        public static readonly Color Bad = Color.FromHex("#f1f518");
        public static readonly Color Good = Color.FromHex("#2bf518");
    }

    public class StatisticStatusToColorConverter
    {
        public static Color FromStatus(StatisticStatus status)
        {
            Color result;
            switch (status)
            {
                case StatisticStatus.VeryBad:
                    result = StatisticStatusColor.VeryBad;
                    break;
                case StatisticStatus.Bad:
                    result = StatisticStatusColor.Bad;
                    break;
                case StatisticStatus.Good:
                    result = StatisticStatusColor.Good;
                    break;
                default:
                    result = StatisticStatusColor.Default;
                    break;
            }

            return result;
        }
    }
}
