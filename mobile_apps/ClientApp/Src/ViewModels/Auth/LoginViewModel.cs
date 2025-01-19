using System.Diagnostics;
using ClientApp.Src.Storage;
using ClientApp.Src.Views.Auth;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;

namespace ClientApp.Src.ViewModels.Auth;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty] private string? error;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(TryLoginCommand))]
    private string login = "";

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(TryLoginCommand))]
    private string password = "";

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task TryLogin()
    {
        Debug.WriteLine(Login); // TODO: DEBUG
        var response = await Fetcher.Auth.Login(Login, Password);

        if (response.IsEmpty)
            Error = "Сервер не вернул данные";

        if (!response.IsEmptyError)
        {
            Error = response.Error!.Comment;
            Debug.WriteLine("Error TryFetchNews: " + Error); // TODO: DEBUG
            return;
        }

        var body = response.Content!;

        AuthData.SaveAuthData(body.Token!, body.User!);
        await Shell.Current.GoToAsync("//Main");
        Provider.AppShell!.SetEnabledTabsAll(true);
    }

    [RelayCommand]
    private async Task GoToSignup()
    {
        await Shell.Current.Navigation.PushModalAsync(new SignupPage());
    }

    private bool CanLogin()
    {
        return Login != ""
               && Password != "";
    }
}