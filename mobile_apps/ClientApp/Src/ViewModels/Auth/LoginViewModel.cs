using System.Diagnostics;
using ClientApp.Src.Storage;
using ClientApp.Src.Views.Auth;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary.Exceptions;
using RequestsLibrary.Routes.API.Auth;

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
        var (response, body, exception) = await LoginRequest.RequestToServer(Login, Password);

        if (body is null && exception is null)
            exception = new RequestException("Сервер не вернул данные: " +
                                             response.StatusCode);

        if (exception is not null)
        {
            Error = exception.Message;
            return;
        }

        AuthData.SaveAuthData(body.Token, body.User);
        await Shell.Current.GoToAsync("//Main");
        Provider.appShell.setEnabledTabsAll(true);
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