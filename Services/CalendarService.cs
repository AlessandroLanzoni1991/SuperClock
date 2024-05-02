using SuperClock.Models;
using System.Collections.ObjectModel;

namespace SuperClock.Services
{
    public class CalendarService
    {

        #region Methods
  
        public static ObservableCollection<DayModel> GetMonthSimple(int year, int month, bool calendarMode)
        {
            var monthDays = DateTime.DaysInMonth(year, month);

            var days = new List<DayModel>();
            for (var i = 1; i <= monthDays; i++)
                days.Add(new ()
                {
                    DateTime = new DateTime(year, month, i),
                    IsDayOfThisMonth = true
                });

            var monthModel = new MonthModel(month, year, days);

            if (calendarMode)
                monthModel.SetForCalendar();

            var daysObservable = new ObservableCollection<DayModel>();

            foreach(var day in monthModel.Days)
                daysObservable.Add(day);

            return
                daysObservable;
        }

        #endregion

    }
}
