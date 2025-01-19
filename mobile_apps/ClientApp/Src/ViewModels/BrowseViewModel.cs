using System.Collections.ObjectModel;
using System.Diagnostics;
using ClientApp.Src.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary.Models;

namespace ClientApp.Src.ViewModels;

public partial class BrowseViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Category> categories = [];
    [ObservableProperty] private Category? selectedCategory = null;
    [ObservableProperty] private ObservableCollection<News> filteredNews = [];

    [ObservableProperty] private int pageSize  = 2;
    [ObservableProperty] private string? error = null;

    public BrowseViewModel()
    {
        _ = FetchCategories();
    }

    private async Task FetchCategories()
    {
        var res = await Provider.CategoriesViewModel.Fetch();

        if (res is null)
            return;

        Categories = res;
    }

    [RelayCommand]
    private async Task FetchSearchNews()
    {
        Debug.WriteLine("Выполняю поиск новостей ...");
    }

    [RelayCommand]
    private async Task FetchMoreNews()
    {
        Debug.WriteLine("Загрузка ещё новостей ...");
    }
}