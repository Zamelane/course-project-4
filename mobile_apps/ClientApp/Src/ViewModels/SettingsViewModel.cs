using ClientApp.Src.Popups;
using ClientApp.Src.Storage;
using ClientApp.Src.Views;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using System.Net;

namespace ClientApp.Src.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [RelayCommand]
    private async Task GoToProfile()
    {
        await Shell.Current.Navigation.PushAsync(new ProfilePage());
    }

    [RelayCommand]
    private async Task GoToMeNews()
    {
        await Shell.Current.Navigation.PushAsync(new MeNewsPage());
    }
}