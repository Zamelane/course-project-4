using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClientApp.Src.ViewModels;
public partial class HomeViewModel : ObservableObject
{
    [RelayCommand]
    private async Task GoToLoginPage()
    {
        await Shell.Current.Navigation.PushModalAsync(new Views.Auth.LoginPage());
    }
}