using ClientApp.Src.Storage;
using ClientApp.Src.ViewModels;
using System.Diagnostics;

namespace ClientApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Provider.appShell = this;
        }

        public void setEnabledTabsAll(bool isVisibly)
        {
            if (BindingContext is TabsViewModel tvm)
                tvm.SetEnabledTabsAll(isVisibly);
        }
    }
}
