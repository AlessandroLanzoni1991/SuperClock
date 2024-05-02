using System.Globalization;

namespace SuperClock.Helpers.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDayName(this DateTime value)
        {
            var dayname = CultureInfo.InstalledUICulture.DateTimeFormat.GetDayName(value.DayOfWeek);
            var titleDay = CultureInfo.InstalledUICulture.TextInfo.ToTitleCase(dayname);

            return
                titleDay;
        }
        public static string ToMonthName(this DateTime value)
        {
            var monthName = CultureInfo.InstalledUICulture.DateTimeFormat.GetMonthName(value.Month);
            var titleCaseMonth = CultureInfo.InstalledUICulture.TextInfo.ToTitleCase(monthName);

            return
                titleCaseMonth;
        }
    }
}
