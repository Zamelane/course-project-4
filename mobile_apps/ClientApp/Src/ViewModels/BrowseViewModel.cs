using System.Collections.ObjectModel;
using ClientApp.Src.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using RequestsLibrary.Responses.Api.Category;

namespace ClientApp.Src.ViewModels;

public partial class BrowseViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<CategoryResponse>? categories;
    [ObservableProperty] private CategoryResponse? selectedCategory = null;

    public BrowseViewModel()
    {
        Utils.Auxiliary.Wait(FetchCategories());
    }

    private async Task FetchCategories()
    {
        Categories = await Provider.CategoriesViewModel.Fetch();
    }
}