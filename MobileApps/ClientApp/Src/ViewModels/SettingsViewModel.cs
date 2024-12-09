using ClientApp.Src.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClientApp.Src.ViewModels;
public partial class SettingsViewModel : ObservableObject
{
    [RelayCommand]
    private async Task TryExit()
    {
        bool isExit = false;
        try
        {
            (var response, var body, var exception) = await RequestsLibrary.Routes.API.Auth.LogoutRequest.RequestToServer();

            if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                throw new Exception("Не удалось выйти, т.к. сервер вернул что-то не то ...");

            isExit = true;
        }
        catch (Exception ex)
        {
            isExit = await Shell.Current.DisplayAlert(
                "Ошибка",
                "Что-то пошло не так.\nВыйти принудительно?",
                "Да", "Отмена"
            );
        }

        if (!isExit)
            return;

        AuthData.Token = null;
        AuthData.User = null;
        Provider.appShell.setEnabledTabsAll(false);
        await Shell.Current.GoToAsync("//Home");
    }
}
