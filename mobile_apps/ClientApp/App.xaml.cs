using ClientApp.Src.Storage;

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

        if (AuthData.User != null && AuthData.Token != null)
            Provider.AppShell.SetEnabledTabsAll(true);
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