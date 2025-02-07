using ClientApp.Src.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using System.Diagnostics;
using System.Text.Json;

namespace ClientApp.Src.ViewModels.Auth;

public partial class SignupViewModel : ObservableObject
{
    [ObservableProperty] private Dictionary<string, List<string>> badFields = [];

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
        var response = await Fetcher.Auth.Register(FirstName, LastName, Login, Password, BirthDay, Email);

        // Проверям на ошибки
        if (response.Error == null && response.Content == null)
            Error = "Сервер не вернул данные";

        if (response.Error is not null)
        {
            Error = response.Error.Comment;
            Debug.WriteLine("Error TryFetchNews: " + Error); // TODO: DEBUG
            Debug.WriteLine("Error TryFetchNews, Content: " + JsonSerializer.Serialize(response.Error)); // TODO: DEBUG

            if (response.Error.ValidationResponse is not null)
                BadFields = response.Error.ValidationResponse.Errors ?? [];
            
            return;
        }

        var body = response.Content!;

        // Если всё ок - сохраняем авторизацию
        Provider.AuthData.SaveAuthData(body.Token!, body.User!);
        await Shell.Current.GoToAsync("//Main");
        Provider.AppShell!.SetEnabledTabsAll(true);
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