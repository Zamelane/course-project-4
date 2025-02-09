using ClientApp.Src.Storage;
using CommunityToolkit.Maui.Alerts;
using RequestsLibrary;

namespace ClientApp;

public partial class App : Application
{
    // Windows: window size
    private const int newWidth = 450;
    private const int newHeight = 800;

    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        if (Provider.AuthData.User != null && Provider.AuthData.Token != null)
            Provider.AppShell.SetEnabledTabsAll(true);

        Fetcher.ErrorConnectedAction = () =>
        {
            //Toast.Make("Превышено время ожидания").Show();
        };
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        window.Width = newWidth;
        window.Height = newHeight;

        window.MinimumWidth = 350;
        window.MinimumHeight = 500;

        return window;
    }
}