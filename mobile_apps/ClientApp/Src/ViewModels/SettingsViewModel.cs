using System.Net;
using ClientApp.Src.Popups;
using ClientApp.Src.Storage;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;

namespace ClientApp.Src.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [RelayCommand]
    private async Task TryExit()
    {
        var isExit = false;
        try
        {
            var response = await Fetcher.Auth.Logout();

            /*if (response.StatusCode != HttpStatusCode.NoContent)
                throw new Exception("Не удалось выйти, т.к. сервер вернул что-то не то ...");*/
            if (response.Error is not null && response.StatusCode != HttpStatusCode.NoContent)
                throw new Exception("Не удалось выйти, т.к. сервер вернул что-то не то: " + response.Error.Comment);

            isExit = true;
        }
        catch(Exception ex)
        {
            var result = await Shell.Current.ShowPopupAsync(
                new QuestionPopup(
                    "Ошибка",
                    $"{ex.Message}.\nВыйти принудительно?"
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
        Provider.AppShell.SetEnabledTabsAll(false);
    }
}