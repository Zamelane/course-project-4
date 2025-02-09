using ClientApp.Src.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;
public partial class FavouriteViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<MinNews> news = [];
    [ObservableProperty] private int total;
    [ObservableProperty] private string? error = null;
    [ObservableProperty] private int pageSize = 2;
    [ObservableProperty] private bool isEndPage = false;
    [ObservableProperty] private bool isFetching = false;
    private int page = 0;
    public string Title
    {
        get
        {
            string title = "Bookmark";

            if (!News.Any())
                return title;

            return title + $" ({News.Count})";
        }
    }

    public FavouriteViewModel()
    {
        GetMoreNewsCommand.Execute(null);
    }
    public bool CanGetMoreNews => !IsEndPage;
    [RelayCommand(CanExecute = nameof(CanGetMoreNews))]
    private async Task GetMoreNews(bool? ignoreIsFetching = null)
    {
        if (ignoreIsFetching != true && (IsEndPage || IsFetching))
            return;

        if (page == 0)
        {
            IsEndPage = false;
            News.Clear();
            Total = 0;
        }

        page++;

        RequestParams rp = new();
        rp.AddParameter("page", page);

        await Auxiliary.RunWithStateHandling(
            () => Fetcher.Favourite.Get(rp),
            _ => IsFetching = _,
            _ => Error = _,
            r =>
            {
                if (r is null){
                    Debug.WriteLine("Ничего не пришло для избранного");
                    page--;
                    return;
                }

                if (r.Favourites.Count == 0)
                {
                    Debug.WriteLine("Избранное пустое");
                    IsEndPage = true;
                }
                else
                    r.Favourites.ToList().ForEach(e => News.Add(e.News));
                Debug.WriteLine($"Favourite news: {News.Count}");

                Total = r.Total;

                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(CanGetMoreNews));
            }
        );
    }

    [RelayCommand]
    private async Task UpdateNewsList()
    {
        page = 0;
        await GetMoreNews(true);
    }
}