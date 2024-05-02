using System.ComponentModel;

namespace SuperClock.Models.UI
{
    public class CityTimeModel : INotifyPropertyChanged
    {
        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (value != city)
                {
                    city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        private string color;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CityTimeModel(string city, string color)
        {
            this.City = city;
            this.Color = color;
        }
    }
}
