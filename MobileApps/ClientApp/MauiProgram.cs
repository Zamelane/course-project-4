using Microsoft.Extensions.Logging;

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
                    fonts.AddFont("OpenSans-Regular.ttf" , "OpenSansRegular" );
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-Medium.ttf"    , "RobotoMedium"    );
                    fonts.AddFont("Roboto-Regular.ttf"   , "RobotoRegular"   );
                    fonts.AddFont("Nunito.ttf"           , "Nunito"          );
                    fonts.AddFont("Nunito-Italic.ttf"    , "Nunito-Italic"   );
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
