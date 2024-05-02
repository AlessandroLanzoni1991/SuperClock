using System.Globalization;

namespace SuperClock.Helpers.Converters
{
    public class IsDayOfTheMonthOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return
                (bool)value ? 1 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw
                new NotImplementedException();
        }
    }
}
