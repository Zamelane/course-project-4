using ClientApp.Src.Storage;
using ClientApp.Src.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ClientApp.Src.ViewModels;

public partial class BrowseViewModel : ObservableObject
{
    public enum Sorts
    {
        FirstNew,
        FirstOld,
        FirstMoreViews,
        Random
    }

    private List<KeyValuePair<Sorts, string>> _sortNames = [
        new(Sorts.FirstNew,       "Сначала новые"     ),
        new(Sorts.FirstOld,       "Сначала старые"    ),
        new(Sorts.FirstMoreViews, "По просмотрам"     ),
        new(Sorts.Random,         "Случайный порядок" )
    ];

    [ObservableProperty] private ObservableCollection<Category> categories          = [];
    [ObservableProperty] private ObservableCollection<Category> selectedCategories  = [];
    [ObservableProperty] private Sorts                          sortBy              = Sorts.FirstNew;
    [ObservableProperty] private ObservableCollection<Tag>      tags                = [];
    [ObservableProperty] private ObservableCollection<MinNews>  filteredNews        = [];
    
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
        _ = FetchTags();
        _ = SelectSort(null);
    }

    [ObservableProperty] private string selectedCategoriesTitle;
    [ObservableProperty] private string selectedOrderTitle;
    public List<string> TypesSortBy
    {
        get => _sortNames.Select(pair => pair.Value).ToList();
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

    private async Task FetchTags()
    {
        await Auxiliary.RunWithStateHandling<ObservableCollection<Tag>?>(
            () => Fetcher.Fetch<ObservableCollection<Tag>>(HttpMethod.Get, Fetcher.Config.GetApiUrl("tags")),
            _ => IsFetching = _,
            _ => Error = _,
            res =>
            {
                if (res is not null)
                    Tags = res;

                //if (res is not null)
                //    SelectedCategories.Add(Categories.First());
            }
        );
    }

    // Тута после ввода поискового запроса
    [RelayCommand]
    private async Task FetchSearchNews()
    {
        Debug.WriteLine("Выполняю поиск новостей ...");
        _ = FetchMoreNews(0);
    }

    [RelayCommand]
    private async Task AllUpdate()
    {
        _ = FetchCategories();
        _ = FetchTags();
        _ = FetchMoreNews(0);
    }

    [ObservableProperty] private bool isNewsFetching = false;
    [RelayCommand]
    private async Task FetchMoreNews(int? page = null)
    {
        page ??= _currentPage;

        if (page == 0)
            IsEndPage = false;

        if (IsEndPage || IsNewsFetching)
            return;

        Debug.WriteLine("Загрузка ещё новостей ...");

        page++; // Загружаем сразу следующую

        // Строим параметры запроса
        RequestParams rp = new();

        if (SearchText.Trim().Length >= 3)
            rp.AddParameter("search", SearchText);

        Debug.WriteLine(SelectedCategories.Count);
        if (SelectedCategories.Any()){
            Debug.WriteLine("Я В КАТЕГОРИЯХ");
            rp.AddParameter("categories", String.Join(',', SelectedCategories.Select(c => c.Id).ToList()));
        }

        rp.AddParameter("sort", SortBy.ToString());

        rp.AddParameter("page", page);

        // Запрашиваем данные
        var response = await Auxiliary.RunWithStateHandling(
            async () => await Fetcher.News.Get(rp),
            _ => isNewsFetching = _,
            _ => Error = _,
            fn =>
            {
                if (fn is null)
                    return;
                if (page != 1)
                    fn.News.ToList().ForEach(FilteredNews.Add);
                else FilteredNews = fn.News;
            }
        );

        Debug.WriteLine(Error);

        // Прверяем, есть ли ошибки и пришло ли тело
        if (!response.IsEmptyError || response.IsEmptyContent)
            return;

        // Если не вернулись элементы, то не загружаем следующие страницы
        if (response.Content?.News.Count == 0)
        {
            IsEndPage = true;
            return;
        }

        // Если ошибок нет и ответ не пустой, то запоминаем текущую страницу
        _currentPage = (int)page;
    }

    [RelayCommand]
    private async Task SelectCategories(object? categories)
    {
        string title = string.Empty;
        SelectedCategories.Clear();
        ((ObservableCollection<object>)categories).ToList().ForEach(
            c => {
                SelectedCategories.Add(((Category)c));
                title += (title == string.Empty ? String.Empty : ", ") + ((Category)c).Name;
            }
        );
        SelectedCategoriesTitle = title;
        _ = FetchMoreNews(0);
    }

    [RelayCommand]
    private async Task SelectSort(object? sort)
    {
        Debug.WriteLine(sort);

        if (sort is not null)
            SelectedOrderTitle = (string)sort;
        else SelectedOrderTitle = _sortNames.FirstOrDefault(s => s.Key == SortBy).Value;
        _sortNames.ForEach(s => SortBy = s.Value == SelectedOrderTitle ? s.Key : SortBy);

        _ = FetchMoreNews(0);
    }
}