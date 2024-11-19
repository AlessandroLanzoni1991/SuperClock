using SuperClock.Helpers.Extensions;
using SuperClock.Models;
using SuperClock.Models.UI;
using SuperClock.Services;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SuperClock.Pages;

public partial class SnowClockPage
{

    #region Properties


    bool isDarkBackground;

    private ObservableCollection<DayModel> days;
    public ObservableCollection<DayModel> Days
    {
        get => days;
        set
        {
            days = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<CityTimeModel> cities = [];
    public ObservableCollection<CityTimeModel> Cities
    {
        get => cities;
        set
        {
            cities = value;
            OnPropertyChanged();
        }
    }

    //Hours
    int hoursToAdd = 0;

    //Image
    private bool isImage2;
    private int imageCounter = 1;

    //Snow 
    public static bool IsSnowTime;
    private double gridWidth;
    private int numberOfSnowflakes = 180;
    private readonly List<SnowFlakeModel> images = [];

    private DateTime currentDate = DateTime.Now;

    private int currentColorCounter = 0;
    readonly List<string> darkBackgroundColors =
    [
        "#FFFFFF",
        "#FFB6C1", // Rosa pastello
        "#00CED1", // Celeste pastello
        "#FFD700", // Giallo pastello
        "#ADFF2F", // Verde pastello
        "#FFA500", // Arancione pastello
        "#EE82EE", // Viola pastello
        "#00FFFF", // Turchese pastello
        "#FFA07A", // Pesca pastello
        "#B0C4DE", // Blu pastello
        "#F8F8FF", // Lavanda pastello
        "#00FF7F", // Verde menta pastello
        "#FFFFF0", // Giallo pallido
        "#32CD32", // Verde lime pastello
        "#FFA07A", // Melone pastello
        "#87CEEB", // Azzurro pastello
        "#FFF8DC", // Giallo limone pastello
        "#AFEEEE", // Ciano pastello
        "#C0C0C0", // Lavanda pallida
        "#FFDAB9", // Pesca pastella
        "#FFB6C1"  // Rosa pallido
    ];
    readonly List<string> lightBackgroundColors =
    [
        "#FFFFFF",
        "#FFC0CB", // Rosa pastello
        "#87CEEB", // Celeste pastello
        "#FFFF00", // Giallo pastello
        "#98FB98", // Verde pastello
        "#FFD700", // Arancione pastello
        "#D8BFD8", // Viola pastello
        "#AFEEEE", // Turchese pastello
        "#FFDAB9", // Pesca pastello
        "#ADD8E6", // Blu pastello
        "#E6E6FA", // Lavanda pastello
        "#98FF98", // Verde menta pastello
        "#FFFFE0", // Giallo pallido
        "#00FF00", // Verde lime pastello
        "#FAD6A5", // Melone pastello
        "#F0FFFF", // Azzurro pastello
        "#FFFACD", // Giallo limone pastello
        "#E0FFFF", // Ciano pastello
        "#D3D3D3", // Lavanda pallida
        "#FFE5B4", // Pesca pastella
        "#FFE4E1"  // Rosa pallido
    ];

    #endregion


    #region Constructor

    public SnowClockPage()
	{
		InitializeComponent();
        this.gridWidth = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) / 2;


        RefreshTime();

        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            RefreshTime();
            return
                true;
        });
        Dispatcher.StartTimer(TimeSpan.FromMilliseconds(100), () =>
        {
            AnimateSnowflakes();
            return
                true;
        });


        this.Cities = new ObservableCollection<CityTimeModel>()
        {
            new("Current", "#FFFFFF"),
            new("New York","#FFFFFF"),
            new("Londra","#FFFFFF"),
            new("Tokyo","#FFFFFF"),
            new("Sydney","#FFFFFF"),
            new("Parigi","#FFFFFF"),
            new("Dubai","#FFFFFF"),
            new("Pechino","#FFFFFF"),
            new("Mosca","#FFFFFF"),
        };


        this.BindingContext = this;
    }

    #endregion


    #region Events

    private void GoPreviusMonth(object sender, TappedEventArgs e)
    {
        this.currentDate = this.currentDate.AddMonths(-1);
        FillCalendar();
    }
    private void GoNextMonth(object sender, TappedEventArgs e)
    {
        this.currentDate = this.currentDate.AddMonths(1);
        FillCalendar();
    }
    private async void ChangeImage(object sender, TappedEventArgs e)
    {
        var label = sender as Label;

        label.Scale = 0.95;

        await
            Task.Delay(40);

        label.Scale = 1;

        ChangeBackground();
    }
    private async void StartStopSnow(object sender, TappedEventArgs e)
    {
        var label = sender as Label;

        label.Scale = 0.95;

        await
            Task.Delay(40);

        label.Scale = 1;

        IsSnowTime = !IsSnowTime;
    }
    private void PlaceLabelClicked(object sender, TappedEventArgs e)
    {
        var label = sender as Label;


        switch (label.Text)
        {
            case "Current":

                hoursToAdd = 1;
                break;

            case "New York":

                hoursToAdd = -5;
                break;

            case "Londra":

                hoursToAdd = 0;
                break;

            case "Tokyo":

                hoursToAdd = 9;
                break;

            case "Sydney":

                hoursToAdd = 10;
                break;

            case "Parigi":

                hoursToAdd = 1;
                break;

            case "Dubai":

                hoursToAdd = 4;
                break;

            case "Mosca":

                hoursToAdd = 3;
                break;
        }

        RefreshTime();
    }
    private void ChangeColorsLabels(object sender, TappedEventArgs e)
    {
        var list = isDarkBackground ? darkBackgroundColors : lightBackgroundColors;

        currentColorCounter++;

        if (currentColorCounter > list.Count - 1)
            currentColorCounter = 0;


        //Buttons
        this.ChangeColorLabel.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.ChangeImageLabel.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.SnowLabel.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.HourLabel.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.MinuteLabel.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.OtherCitiesLabel.TextColor = Color.FromRgba(list[currentColorCounter]);

        //Cities
        this.PreviusMonthButt.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.LabelDate.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.NextMonthButton.TextColor = Color.FromRgba(list[currentColorCounter]);

        //Legenda days
        this.Day1.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.Day2.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.Day3.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.Day4.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.Day5.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.Day6.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.Day7.TextColor = Color.FromRgba(list[currentColorCounter]);

        //Clock 
        this.DayNumber.TextColor = Color.FromRgba(list[currentColorCounter]);
        this.MonthName.TextColor = Color.FromRgba(list[currentColorCounter]);


        foreach (var citie in this.Cities)
            citie.Color = list[currentColorCounter];

        foreach (var day in this.Days)
            day.Color = list[currentColorCounter];
    }


    //Effects
    private void GenerateImages()
    {
        Random random = new Random();

        for (int i = 0; i < numberOfSnowflakes; i++)
        {
            var size = random.Next(10, 17);
            var imagenumber = random.Next(0, 4);
            var imageName = $"snowflake{imagenumber}";

            images.Add(new SnowFlakeModel
            {
                Source = imageName,
                HeightRequest = size,
                VerticalOptions = LayoutOptions.Start,
                TranslationY = -50,
                TranslationX = random.Next((int)-gridWidth, (int)gridWidth)
            });
        }


        foreach (var image in images)
            this.SnowGrid.Children.Add(image);
    }
    private void AnimateSnowflakes()
    {
        if (!IsSnowTime)
            return;

        foreach (SnowFlakeModel image in images)
        {
            if (image.IsAnimating)
                continue;


            if (!image.IsTimeForGo())
                continue;


            image.Animate();
        }
    }
    private async void ChangeBackground()
    {
        if (isImage2)
        {
            isImage2 = false;

            this.Image1.Source = $"background{imageCounter}";

            await
                Task.Delay(100);

            var t1 = this.Image1.FadeTo(1, 1000, Easing.Linear);
            var t2 = this.Image2.FadeTo(0, 1000, Easing.Linear);

            await
                Task.WhenAll(t1, t2);
        }
        else
        {
            isImage2 = true;

            this.Image2.Source = $"background{imageCounter}";

            await
                Task.Delay(100);

            var t1 = this.Image1.FadeTo(0, 1000, Easing.Linear);
            var t2 = this.Image2.FadeTo(1, 1000, Easing.Linear);

            await
                Task.WhenAll(t1,t2);
        }

        this.imageCounter++;

        if(imageCounter == 11)
            imageCounter = 0;

        this.isDarkBackground = imageCounter >= 5;
    }

    #endregion


    #region Methods

    private void FillCalendar()
    {
        try
        {
            this.Days = CalendarService.GetMonthSimple(currentDate.Year, currentDate.Month, true);
            this.LabelDate.Text = $"{this.currentDate.ToMonthName()} {this.currentDate.Year}";
        }
        catch
        {
            //Ignore
        }
    }
    private void RefreshTime()
    {
        this.HourLabel.Text = DateTime.Now.AddHours(hoursToAdd).ToString("HH");
        this.MinuteLabel.Text = DateTime.Now.AddHours(hoursToAdd).ToString("mm");

        this.DayNumber.Text = DateTime.Now.AddHours(hoursToAdd).ToString("dd");
        this.MonthName.Text = CultureInfo.InstalledUICulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month));
    }

    #endregion


    #region Overrides

    protected override void OnAppearing()
    {
        base.OnAppearing();
        FillCalendar();

        GenerateImages();
    }

    #endregion

}