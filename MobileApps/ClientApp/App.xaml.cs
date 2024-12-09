using ClientApp.Src.Storage;

namespace ClientApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            if (AuthData.User != null && AuthData.Token != null)
                Provider.appShell.setEnabledTabsAll(true);
        }
    }
}
