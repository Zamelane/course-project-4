using ClientApp.Src.Storage;
using ClientApp.Src.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;

public partial class BrowseViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Category> categories = [];
    [ObservableProperty] private ObservableCollection<Category> selectedCategories = [];
    [ObservableProperty] private ObservableCollection<MinNews> filteredNews = [];
    
    [NotifyCanExecuteChangedFor(nameof(FetchSearchNewsCommand))]
    [NotifyCanExecuteChangedFor(nameof(FetchMoreNewsCommand))]
    [ObservableProperty] private bool isFetching = false;

    [ObservableProperty] private int pageSize       = 2;
    [ObservableProperty] private bool isEndPage     = false;
    [ObservableProperty] private string searchText  = "";
    [ObservableProperty] private string? error      = null;
    private int _currentPage = 0;

    public BrowseViewModel()
    {
        _ = FetchCategories();
        _ = FetchMoreNews();
    }

    private async Task FetchCategories()
    {
        await Auxiliary.RunWithStateHandlingWithoutResponse<ObservableCollection<Category>?>(
            () => Provider.CategoriesViewModel.Fetch(),
            _ => IsFetching = _,
            _ => Error = _,
            res =>
            {
                if (res is not null)
                    Categories = res;

                //if (res is not null)
                //    SelectedCategories.Add(Categories.First());
            }
        );
    }

    // Тута после ввода поискового запроса
    [RelayCommand(CanExecute = nameof(CanFetch))]
    private async Task FetchSearchNews()
    {
        Debug.WriteLine("Выполняю поиск новостей ...");
        await FetchMoreNews(0);
    }

    [RelayCommand(CanExecute = nameof(CanFetch))]
    private async Task FetchMoreNews(int? page = null)
    {
        page ??= _currentPage;

        if (page == 0)
            IsEndPage = false;

        if (IsEndPage)
            return;

        Debug.WriteLine("Загрузка ещё новостей ...");

        page++; // Загружаем сразу следующую

        // Строим параметры запроса
        RequestParams rp = new();

        if (SearchText.Trim().Length >= 3)
            rp.AddParameter("search", SearchText);

        /*if (SelectedCategory is not null)
            rp.AddParameter("category", SelectedCategory.Id);*/

        rp.AddParameter("page", page);

        // Запрашиваем данные
        var response = await Auxiliary.RunWithStateHandling(
            async () => await Fetcher.News.Get(rp),
            _ => IsFetching = _,
            _ => Error = _,
            fn =>
            {
                if (fn is null)
                    return;
                if (page != 1)
                    fn.ToList().ForEach(FilteredNews.Add);
                else FilteredNews = fn;
            }
        );

        Debug.WriteLine(Error);

        // Прверяем, есть ли ошибки и пришло ли тело
        if (!response.IsEmptyError || response.IsEmptyContent)
            return;

        // Если не вернулись элементы, то не загружаем следующие страницы
        if (response.Content?.Count == 0)
        {
            IsEndPage = true;
            return;
        }

        // Если ошибок нет и ответ не пустой, то запоминаем текущую страницу
        _currentPage = (int)page;
    }

    private bool CanFetch() => !IsFetching;
}