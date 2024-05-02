using System.Globalization;

namespace SuperClock.Models
{
    public class MonthModel(int monthNumber, int year, List<DayModel> days)
    {

        #region Properties

        public string Name { get; set; } = CultureInfo.InstalledUICulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNumber));
        public DateTime DateTime { get; set; } = new DateTime(year, monthNumber, 1);
        public List<DayModel> Days { get; set; } = days;
        
        #endregion


        #region Methods

        public void SetForCalendar()
        {
            var firstDay = this.Days.First().DateTime;
            var lastDay = this.Days.Last().DateTime;

            //Add before days
            while (lastDay.DayOfWeek != DayOfWeek.Sunday)
            {
                var newDay = new DayModel()
                {
                    DateTime = lastDay.AddDays(1),
                    IsDayOfThisMonth = false
                };

                this.Days.Add(newDay);

                lastDay = newDay.DateTime;
            }

            //Add after days
            while (firstDay.DayOfWeek != DayOfWeek.Monday)
            {
                var newDay = new DayModel()
                {
                    DateTime = firstDay.AddDays(-1),
                    IsDayOfThisMonth = false
                };

                this.Days.Add(newDay);

                firstDay = newDay.DateTime;
            }

            //Order month for date time
            this.Days = [.. this.Days.OrderBy(x => x.DateTime)];
        }

        #endregion

    }
}
