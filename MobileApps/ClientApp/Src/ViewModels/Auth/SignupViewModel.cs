using ClientApp.Src.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ObservableDictionary;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels.Auth
{
    public partial class SignupViewModel: ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
        private string firstName = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
        private string lastName = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
        private string login = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
        private string password = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
        private string passwordConfirm = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
        private DateTime birthDay;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(TrySignUpCommand))]
        private string email = "";

        [ObservableProperty]
        private string? error = null;

        [ObservableProperty]
        private ObservableStringDictionary<List<string>> badFields = [];

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
                return;
            }

            // Пробуем зарегаться
            (var response, var body, var exception) 
                = await RequestsLibrary.Routes.API.Auth
                .SignupRequest.RequestToServer(FirstName, LastName, Login, Password, BirthDay, Email);

            // Проверям на ошибки
            if (body is null && exception is null)
                exception = new RequestsLibrary.Exceptions.RequestException("Сервер не вернул данные: " + response.StatusCode);

            if (exception is not null)
            {
                Error = exception.Message;
                if (body?.Errors != null)
                    BadFields = body.Errors;
                return;
            }

            // Если всё ок - сохраняем авторизацию
            Storage.AuthData.SaveAuthData(body.Token, body.User);
            Provider.appShell.setEnabledTabsAll(true);
            await Shell.Current.GoToAsync("//Main");
        }

        private bool CanSignup() =>
            FirstName != String.Empty
            && LastName != String.Empty
            && Login != String.Empty
            && Password != String.Empty
            && Email != String.Empty;

        [RelayCommand]
        private async Task GoToLogin()
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
