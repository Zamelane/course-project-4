using System.Net;
using ClientApp.Src.Popups;
using ClientApp.Src.Storage;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary.Routes.API.Auth;

namespace ClientApp.Src.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [RelayCommand]
    private async Task TryExit()
    {
        var isExit = false;
        try
        {
            var (response, body, exception) = await LogoutRequest.RequestToServer();

            if (response.StatusCode != HttpStatusCode.NoContent)
                throw new Exception("Не удалось выйти, т.к. сервер вернул что-то не то ...");

            isExit = true;
        }
        catch
        {
            var result = await Shell.Current.ShowPopupAsync(
                new QuestionPopup(
                    "Ошибка",
                    "Что-то пошло не так.\nВыйти принудительно?"
                ), CancellationToken.None
            );
            
            if (result is bool boolResult)
                isExit = boolResult;
        }

        if (!isExit)
            return;

        AuthData.Token = null;
        AuthData.User = null;
        await Shell.Current.GoToAsync("//Main");
        Provider.appShell.setEnabledTabsAll(false);
    }
}