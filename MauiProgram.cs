using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace SuperClock
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");


                    fonts.AddFont("Roboto-Black.ttf", "RobotoBlack");

                    fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
                    
                    fonts.AddFont("Roboto-Light.ttf", "RobotoLight");
                    
                    fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
                    
                    fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");

                    //Icons
                    fonts.AddFont("fa-solid-900.ttf", "FABrands");
                })
                .ConfigureLifecycleEvents(events =>
                {
                    #if ANDROID
                        
                    events.AddAndroid(android => android.OnCreate((activity, bundle) => MakeStatusBarTranslucent(activity)));

                    static void MakeStatusBarTranslucent(Android.App.Activity activity)
                    {
                        activity.Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);

                        activity.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);

                        activity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
                    }
                    #endif
                }); ;

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
