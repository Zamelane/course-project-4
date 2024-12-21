using ClientApp.Src.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary.Routes.API.Auth;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels.Auth;
public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(TryLoginCommand))]
    private string login = "";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(TryLoginCommand))]
    private string password = "";

    [ObservableProperty]
    private string? error = null;

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task TryLogin()
    {
        Debug.WriteLine(Login); // TODO: DEBUG
        (var response, var body, var exception) = await LoginRequest.RequestToServer(Login, Password);

        if (body is null && exception is null)
            exception = new RequestsLibrary.Exceptions.RequestException("Сервер не вернул данные: " + response.StatusCode);

        if (exception is not null)
        {
            Error = exception.Message;
            return;
        }

        Storage.AuthData.SaveAuthData(body.Token, body.User);
        Provider.appShell.setEnabledTabsAll(true);
        await Shell.Current.GoToAsync("//Main");
    }

    [RelayCommand]
    private async Task GoToSignup() => await Shell.Current.Navigation.PushModalAsync(new Src.Views.Auth.SignupPage());

    private bool CanLogin() =>
        Login != ""
        && Password != "";
}