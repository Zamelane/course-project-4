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
    [ObservableProperty] private bool isFetchingProcess = false;
    private int page = 0;
    public string Title
    {
        get
        {
            string title = "Закладки";

            return title + $" ({Total})";
        }
    }

    public FavouriteViewModel()
    {
        GetMoreNewsCommand.Execute(null);
    }
    public bool CanGetMoreNews => !IsEndPage;
    [RelayCommand(CanExecute = nameof(CanGetMoreNews))]
    private async Task GetMoreNews(int? loadPage = null)
    {
        if ((IsEndPage && loadPage is null) || IsFetchingProcess)
            return;

        IsFetchingProcess = true;

        if (loadPage is not null)
        {
            page = (int)loadPage;
            IsEndPage = false;
            News.Clear();
        }

        page++;

        RequestParams rp = new();
        rp.AddParameter("page", page);

        await Auxiliary.RunWithStateHandling(
            () => Fetcher.Favourite.Get(rp),
            _ => {
                IsFetching = _;
                IsFetchingProcess = _;
            },
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
                {
                    r.Favourites.ToList().ForEach(e => News.Add(e.News));
                }    
                Debug.WriteLine($"Favourite news: {News.Count}");

                Total = r.Total;

                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(CanGetMoreNews));
            }
        );
        IsFetchingProcess = false;
    }

    [RelayCommand]
    private async Task UpdateNewsList()
    {
        await GetMoreNews(0);
    }
}