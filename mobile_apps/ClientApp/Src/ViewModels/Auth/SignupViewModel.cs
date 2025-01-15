using ClientApp.Src.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ObservableDictionary;
using RequestsLibrary.Exceptions;
using RequestsLibrary.Routes.API.Auth;

namespace ClientApp.Src.ViewModels.Auth;

public partial class SignupViewModel : ObservableObject
{
    [ObservableProperty] private ObservableStringDictionary<List<string>> badFields = [];

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
    private DateTime birthDay;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
    private string email = "";

    [ObservableProperty] private string? error;

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
    private string firstName = "";

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
    private string lastName = "";

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
    private string login = "";

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
    private string password = "";

    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
    private string passwordConfirm = "";

    [RelayCommand(CanExecute = nameof(CanSignup))]
    private async Task TrySignUp()
    {
        // Сбрасываем статусы ошибок
        Error = null;
        BadFields = [];

        // Проводим валидацию перед отправкой
        if (Password != PasswordConfirm)
        {
            Error = "Ошибка валидации данных";
            BadFields.Add("passwordConfirm", ["Пароли не совпадают"]);
            OnPropertyChanged(nameof(BadFields));
            return;
        }

        // Пробуем зарегаться
        var (response, body, exception)
            = await SignupRequest.RequestToServer(FirstName, LastName, Login, Password, BirthDay, Email);

        // Проверям на ошибки
        if (body is null && exception is null)
            exception = new RequestException("Сервер не вернул данные: " +
                                             response.StatusCode);

        if (exception is not null)
        {
            Error = exception.Message;
            if (body?.Errors != null)
                BadFields = body.Errors;
            return;
        }

        // Если всё ок - сохраняем авторизацию
        AuthData.SaveAuthData(body.Token, body.User);
        Provider.AppShell.setEnabledTabsAll(true);
        await Shell.Current.GoToAsync("//Main");
    }

    private bool CanSignup()
    {
        return FirstName != string.Empty
               && LastName != string.Empty
               && Login != string.Empty
               && Password != string.Empty
               && PasswordConfirm != string.Empty
               && Email != string.Empty;
    }

    [RelayCommand]
    private async Task GoToLogin()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}