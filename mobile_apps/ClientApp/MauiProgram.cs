using ClientApp.Src.Controls;
using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Maui.Handlers;

namespace ClientApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseFFImageLoading()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
                fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                fonts.AddFont("Nunito.ttf", "Nunito");
                fonts.AddFont("Nunito-Italic.ttf", "Nunito-Italic");
            });

        EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
        {
#if ANDROID
                   handler.PlatformView.Background = null;
                   handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS || MACCATALYST
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
            handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
        });

        ToolbarHandler.Mapper.AppendToMapping("CustomNavigationView", (handler, view) =>
        {
#if ANDROID
        handler.PlatformView.ContentInsetStartWithNavigation = 0;
        handler.PlatformView.SetContentInsetsAbsolute(0, 0);
#endif
        });

        return builder.Build();
    }
}