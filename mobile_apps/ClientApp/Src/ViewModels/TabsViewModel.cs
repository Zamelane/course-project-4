using ClientApp.Src.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;

public partial class TabsViewModel : ObservableObject
{
    [ObservableProperty] private bool bookmarkIsEnabled;

    [ObservableProperty] private bool loginIsVisible = true;

    [ObservableProperty] private bool settingsIsEnabled;

    [ObservableProperty] private bool notificationIsVisible = false;

    public void SetEnabledTabsAll(bool isVisibly)
    {
        BookmarkIsEnabled = isVisibly;
        SettingsIsEnabled = isVisibly;
        LoginIsVisible = !isVisibly;
        NotificationIsVisible = Provider.AuthData?.User?.Role == "reporter";
        Debug.WriteLine("Role: " + Provider.AuthData?.User?.Role);
        Debug.WriteLine($"{BookmarkIsEnabled}, {SettingsIsEnabled}, {LoginIsVisible}");
    }
}