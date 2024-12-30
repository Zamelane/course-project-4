using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;
public partial class TabsViewModel : ObservableObject
{
    [ObservableProperty]
    private bool bookmarkIsEnabled = false;

    [ObservableProperty]
    private bool settingsIsEnabled = false;

    [ObservableProperty]
    private bool loginIsVisible = true;

    public void SetEnabledTabsAll(bool isVisibly)
    {
        BookmarkIsEnabled = isVisibly;
        SettingsIsEnabled = isVisibly;
        LoginIsVisible    = !isVisibly;
        Debug.WriteLine($"{BookmarkIsEnabled}, {SettingsIsEnabled}, {LoginIsVisible}");
    }
}