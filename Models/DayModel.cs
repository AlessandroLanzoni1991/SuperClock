using System.ComponentModel;
using System.Globalization;

namespace SuperClock.Models
{
    public class DayModel : INotifyPropertyChanged
    {
        #region Properties

        private string color = "#FFFFFF";
        public string Color
        {
            get { return color; }
            set
            {
                if (value != color)
                {
                    color = value;
                    OnPropertyChanged(nameof(Color));
                }
            }
        }

        public string Date => CultureInfo.InstalledUICulture.TextInfo.ToTitleCase(CultureInfo.InstalledUICulture.DateTimeFormat.GetDayName(DateTime.DayOfWeek)) + " " + DateTime.Day;
        public DateTime DateTime { get; set; }
        public bool IsDayOfThisMonth { get; set; }

        #endregion


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        #region Constructor

        public DayModel()
        {
        }

        #endregion

    }
}
