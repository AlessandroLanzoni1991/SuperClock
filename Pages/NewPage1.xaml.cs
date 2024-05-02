using SuperClock.Models.UI;

namespace SuperClock.Pages;

public partial class NewPage1 : ContentPage
{

    private double gridWidth;
    private int numberOfSnow = 130;


    private List<SnowFlakeModel> images = [];

	public NewPage1()
	{
		InitializeComponent();

        this.gridWidth = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) / 2;
    }


    private void GenerateImages()
    {
        Random random = new Random();

        // Genera 50 immagini
        for (int i = 0; i < numberOfSnow; i++)
        {
            var r = new Random();

            var size = random.Next(10,17);
            var imagenumber = random.Next(0, 4);
            var imageName = $"snowflake{imagenumber}";

            images.Add(new SnowFlakeModel
            {
                Source = imageName,
                HeightRequest = size,
                VerticalOptions = LayoutOptions.Start,
                TranslationY = -50,
                TranslationX = random.Next((int)-gridWidth, (int)gridWidth),
                StartingTranslationX = TranslationX
            });
        }
        

        foreach(var image in images)
            this.MainGrid.Children.Add(image);
    }
    private void AnimateSnowflakes()
    {
        foreach (SnowFlakeModel image in images)
        {
            if (image.IsAnimating)
                continue;


            if (!image.IsTimeForGo())
                continue;


            image.Animate();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        GenerateImages();

        Dispatcher.StartTimer(TimeSpan.FromMilliseconds(100), () =>
        {
            AnimateSnowflakes();
            return
                true;
        });
    }


}