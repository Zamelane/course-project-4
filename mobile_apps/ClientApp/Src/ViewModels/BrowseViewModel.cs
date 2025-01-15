using System.Collections.ObjectModel;
using ClientApp.Src.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using RequestsLibrary.Responses.Api.Category;

namespace ClientApp.Src.ViewModels;

public partial class BrowseViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<CategoryResponse> categories = [];
    [ObservableProperty] private CategoryResponse? selectedCategory = null;

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
}