using PulseOximeterApp.Data.DataBase;
using System;
using Xamarin.Forms;

namespace PulseOximeterApp.Infrastructure.Converters
{
    public class StatisticStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Color))
            {
                throw new InvalidOperationException("The target must be a Color!");
            }

            return Services.Colors.StatisticStatusToColorConverter.FromStatus((StatisticStatus)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
