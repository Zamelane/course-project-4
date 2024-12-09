using CommunityToolkit.Mvvm.ComponentModel;

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
    }
}