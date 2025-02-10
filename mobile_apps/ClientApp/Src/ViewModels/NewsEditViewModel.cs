using ClientApp.Src.Popups;
using ClientApp.Src.Storage;
using ClientApp.Src.Utils;
using CommunityToolkit.Maui.Views;
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
    [ObservableProperty] private FullNews? editableNews = null;
    [ObservableProperty] private Dictionary<string, List<string>> badFields = [];

    [ObservableProperty] private ObservableCollection<Category> categories = [];
    [ObservableProperty] private Category? selectedCategory = null;

    [ObservableProperty] private bool isFetched = false;
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    [ObservableProperty] private bool isEdit = false;


    public string PageTitle
    {
        get
        {
            if (EditableNews is not null)
                SelectedCategory = EditableNews!.Category;
            return IsEdit ? "Редактирование новости": "Создание новости";
        }
    }

    [RelayCommand]
    private void OnLoadFullNews()
    {

    }

    [RelayCommand]
    private async Task OpenMenu()
    {
        var result = await Shell.Current.CurrentPage.DisplayActionSheet(
            "Опции",
            "Отмена",
            "Ок",
            "Опубликовать",
            "Загрузить картинку"
        );

        if (result == "Опубликовать")
            await UpdateNews();

        if (result == "Загрузить картинку")
            await UploadPhoto();
    }

    public NewsEditViewModel() : base() { }

    public NewsEditViewModel(FullNews? editableNews = null)
    {
        _ = FetchCategories();
        IsEdit = editableNews is not null;
        EditableNews = editableNews ?? new();
    }

    private async Task FetchCategories()
    {
        await Auxiliary.RunWithStateHandlingWithoutResponse<ObservableCollection<Category>?>(
            () => Provider.CategoriesViewModel.Fetch(),
            null,
            null,
            res =>
            {
                if (res is not null){
                    Categories = res;
                    SelectedCategory ??= res.FirstOrDefault();
                }

                //if (res is not null)
                //    SelectedCategories.Add(Categories.First());
            }
        );
    }

    private async Task UpdateNews()
    {
        if (SelectedCategory is null)
            return;

        string url = Fetcher.Config.GetApiUrl("news") + 
            (EditableNews?.Id != 0 ? $"/{EditableNews?.Id}" : "");

        RequestParams rp = new()
        {
            Body = new NewsUpdateRequest(EditableNews?.Title ?? "", EditableNews?.Content ?? "", SelectedCategory.Id, EditableNews?.Cover?.Hash)
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

    [RelayCommand]
    public async Task SelectCategory(Category? category)
    {
        Debug.WriteLine(category);
        if (category is not null)
            SelectedCategory = category;
    }

    [RelayCommand]
    public async Task UploadPhoto()
    {
        var image = await ImageUploader.SelectAndValidateImageAsync();

        if (image is not null)
        {
            await Clipboard.Default.SetTextAsync($"![{image.Hash}](:api:{image.Url})");

            await Shell.Current.ShowPopupAsync(
                new QuestionPopup(
                    "Изображение загружено",
                    $"Сохранили строку markdown с ссылкой в буфер обмена :)"
                ), CancellationToken.None
            );
        }
        else
        {
            await Shell.Current.ShowPopupAsync(
                new QuestionPopup(
                    "Ошибка",
                    $"Не смогли загрузить выбранное изображение :/"
                ), CancellationToken.None
            );
        }
    }
}