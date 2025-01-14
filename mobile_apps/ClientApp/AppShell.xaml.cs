using ClientApp.Src.Storage;
using ClientApp.Src.ViewModels;

namespace ClientApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Provider.AppShell = this;
    }

    public void setEnabledTabsAll(bool isVisibly)
    {
        if (BindingContext is TabsViewModel tvm)
            tvm.SetEnabledTabsAll(isVisibly);
    }
}