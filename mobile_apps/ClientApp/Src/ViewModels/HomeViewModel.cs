using System.Collections.ObjectModel;
using System.Diagnostics;
using ClientApp.Src.Utils;
using ClientApp.Src.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;

namespace ClientApp.Src.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Category>? categories;
    [ObservableProperty] private string? error;
    [ObservableProperty] private bool isFetching;

    [ObservableProperty] private ObservableCollection<MinNews>? mostReadNewses;
    [ObservableProperty] private MinNews? mostReadNewsTop;

    public HomeViewModel()
    {
        Task.Run(TryFetch);
    }

    [RelayCommand]
    private async Task TryFetch()
    {
        await TryFetchNews();
        Categories = await new CategoriesViewModel().Fetch();
        //Debug.WriteLine(Categories);
    }

    private async Task TryFetchNews()
    {
        Debug.WriteLine("Start TryFetchNews"); // TODO: DEBUG

        await Auxiliary.RunWithStateHandling(
            () => Fetcher.News.Get(),
            _ => IsFetching = _,
            _ => Error = _,
            (Action<ObservableCollection<MinNews>?>?)(            newses =>
            {
                if (newses is not null && newses!.Count > 0)
                {
                    MostReadNewsTop = newses.First();
                    newses.Remove((MinNews)MostReadNewsTop);
                    MostReadNewses = newses;
                }
            })
        );

        Debug.WriteLine("End TryFetchNews"); // TODO: DEBUG
    }

    [RelayCommand]
    private void ShowCategoriesPage()
    {
        Shell.Current.Navigation.PushAsync(new CategoriesPage());
    }
}