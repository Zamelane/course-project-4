using ClientApp.Src.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using RequestsLibrary.Requests;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;
public partial class NewsEditViewModel : ObservableObject
{
    [ObservableProperty] private RequestsLibrary.Models.Image? cover = null;
    [ObservableProperty] private string title;
    [ObservableProperty] private string content;

    [ObservableProperty] private bool isFetched = false;

    [RelayCommand]
    private async Task OpenMenu()
    {
        var result = await Shell.Current.CurrentPage.DisplayActionSheet(
            "Опции",
            "Отмена",
            "Ок",
            "Сохранить",
            "Опубликовать",
            "Скрыть/Показать обложку"
        );

        if (result == "Опубликовать")
            await UpdateNews();
    }

    private async Task UpdateNews()
    {
        string url = Fetcher.Config.GetApiUrl("news");

        RequestParams rp = new()
        {
            Body = new NewsUpdateRequest(Title, Content, Cover?.Hash)
        };

        var result = await Auxiliary.RunWithStateHandling(
            async () => await Fetcher.Fetch<FullNews>(HttpMethod.Post, url, rp),
            _ => IsFetched = _,
            _ =>
            {
                Debug.WriteLine(_);
            },
            r =>
            {
                if (r is null)
                    return;

                Shell.Current.Navigation.PopAsync();
            }
        );
    }

    [RelayCommand]
    public async Task SelectAndValidateImageAsync()
    {
        var image = await ImageUploader.SelectAndValidateImageAsync();

        if (image is not null)
            Cover = image;
    }
}