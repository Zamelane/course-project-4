using ClientApp.Src.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Category>? categories;
    [ObservableProperty] private string? error;
    [ObservableProperty] private bool isFetching;

    [ObservableProperty] private ObservableCollection<News>? mostReadNewses;
    [ObservableProperty] private News? mostReadNewsTop;

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
            newses =>
            {
                if (newses is not null && newses!.Count > 0)
                {
                    MostReadNewsTop = newses.First();
                    newses.Remove(MostReadNewsTop);
                    MostReadNewses = newses;
                }
            }
        );

        Debug.WriteLine("End TryFetchNews"); // TODO: DEBUG
    }
}