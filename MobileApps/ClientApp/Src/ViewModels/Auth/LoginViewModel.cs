using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary.Routes.API.Auth;

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
    private async void TryLogin()
    {
        (var response, var body, var exception) = await new LoginRequest(Login, Password).RequestToServer();

        if (body is null && exception is null)
            exception = new RequestsLibrary.Exceptions.RequestException("Сервер не вернул данные");

        if (exception is not null)
        {
            Error = exception.Message;
            return;
        }
    }







    private bool CanLogin() => true;
}