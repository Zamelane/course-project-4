using ClientApp.Src.Storage;
using ClientApp.Src.ViewModels;
using RequestsLibrary;

namespace ClientApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Provider.AppShell = this;
        Fetcher.SetToken(AuthData.Token);
    }

    public void SetEnabledTabsAll(bool isVisibly)
    {
        if (BindingContext is TabsViewModel tvm)
            tvm.SetEnabledTabsAll(isVisibly);
    }
}