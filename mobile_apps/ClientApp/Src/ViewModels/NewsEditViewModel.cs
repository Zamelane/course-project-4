using ClientApp.Src.Storage;
using ClientApp.Src.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using RequestsLibrary.Requests;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;
public partial class NewsEditViewModel : ObservableObject
{
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    [ObservableProperty] private FullNews editableNews;
    [ObservableProperty] private Dictionary<string, List<string>> badFields = [];

    [ObservableProperty] private ObservableCollection<Category> categories = [];
    [ObservableProperty] private Category? selectedCategory = null;

    [ObservableProperty] private bool isFetched = false;


    public string PageTitle => EditableNews is null ? "Создание новости" : "Редактирование новости";
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

    public NewsEditViewModel()
    {
        _ = FetchCategories();
    }

    private async Task FetchCategories()
    {
        await Auxiliary.RunWithStateHandlingWithoutResponse<ObservableCollection<Category>?>(
            () => Provider.CategoriesViewModel.Fetch(),
            null,
            null,
            res =>
            {
                if (res is not null)
                    Categories = res;

                //if (res is not null)
                //    SelectedCategories.Add(Categories.First());
            }
        );
    }

    private async Task UpdateNews()
    {
        string url = Fetcher.Config.GetApiUrl("news") + 
            (EditableNews is not null ? $"/{EditableNews.Id}" : "");

        RequestParams rp = new()
        {
            Body = new NewsUpdateRequest(EditableNews?.Title ?? "", EditableNews?.Content ?? "", EditableNews?.Cover?.Hash)
        };

        BadFields = [];

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

                if (EditableNews is null)
                    Shell.Current.Navigation.PopToRootAsync();
                else
                    Shell.Current.Navigation.PopAsync();
            }
        );

        if (result?.Error?.ValidationResponse is not null)
            BadFields = result.Error.ValidationResponse.Errors ?? [];
    }

    [RelayCommand]
    public async Task SelectAndValidateImageAsync()
    {
        var image = await ImageUploader.SelectAndValidateImageAsync();

        if (image is not null){
            EditableNews.Cover = image;
            OnPropertyChanged(nameof(EditableNews));
        }
    }
}