using ClientApp.Src.Controls;

namespace ClientApp
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
                    fonts.AddFont("OpenSans-Regular.ttf"  , "OpenSansRegular" );
                    fonts.AddFont("OpenSans-Semibold.ttf" , "OpenSansSemibold");
                    fonts.AddFont("Roboto-Medium.ttf"     , "RobotoMedium"    );
                    fonts.AddFont("Roboto-Regular.ttf"    , "RobotoRegular"   );
                    fonts.AddFont("Nunito.ttf"            , "Nunito"          );
                    fonts.AddFont("Nunito-Italic.ttf"     , "Nunito-Italic"   );
                });

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("Borderless", (handler, view) =>
            {
                if (view is BorderlessEntry)
                {
#if ANDROID
                   handler.PlatformView.Background=null;
                   handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS || MACCATALYST
                        handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                        handler.PlatformView.Layer.BorderWidth= 0;
                        handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
                    handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
                }

            });

            return builder.Build();
        }
    }
}
