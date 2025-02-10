using ClientApp.Src.Storage;
using ClientApp.Src.Utils;
using ClientApp.Src.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using RequestsLibrary.Responses;
using System;
using System.Collections.ObjectModel;
using System.Net;

namespace ClientApp.Src.ViewModels;
public partial class NewsPageViewModel : ObservableObject
{
    [ObservableProperty] private MinNews? news;
    [ObservableProperty] private ObservableCollection<MinNews> randomNews = [];
    [NotifyCanExecuteChangedFor(nameof(AddToBookmarksCommand))]
    [NotifyPropertyChangedFor(nameof(CanAddToBookmarks))]
    [ObservableProperty] private FullNews? fullNews;
    [ObservableProperty] private bool isEditVisible = false;

    // Вычисляемые поля
    public string? ReadTime
    {
        get => FullNews is null ? null : Auxiliary.SymbolsToReadTime(FullNews.Content!.Length);
    }

    // Для запросов
    [ObservableProperty] private bool isFetching = false;
    [ObservableProperty] private string? error = null;

    [RelayCommand]
    private async Task UpdateNews()
    {
        if (News is null || IsFetching)
            return;

        // Запрашиваем полностью новость
        await Auxiliary.RunWithStateHandling<FullNews?>(
            () => Fetcher.News.Get(News.Id),
            _ => IsFetching = _,
            _ => Error = _,
            r =>
            {
                FullNews = r ?? FullNews;
                if (FullNews?.Author?.Id == Provider.AuthData?.User?.Id
                    && FullNews?.Author != null && Provider.AuthData?.User != null)
                    IsEditVisible = true;

                ChangedAll();
            }
        );

        await FetchRandomNews();
    }

    public async Task FetchRandomNews()
    {
        RequestParams rpRANDOM = new();
        rpRANDOM.AddParameter("sort", "Random");
        rpRANDOM.AddParameter("limit", "4");


        await Auxiliary.RunWithStateHandling<NewsResponse>(
            () => Fetcher.News.Get(rpRANDOM),
            null,
            _ => Error = _,
            newses =>
            {
                if (newses is not null && newses!.News.Count > 0)
                {
                    RandomNews = newses.News;
                }
            }
        );
    }

    [RelayCommand]
    private async Task ShareNews()
    {
        await Share.RequestAsync(new ShareTextRequest
        {
            Uri = Fetcher.Config.GetApiUrl("news/" + News?.Id),
            Title = "Share Web Link To The News"
        });
    }

    public bool CanAddToBookmarks => News is not null
        && FullNews is not null
        && Provider.AuthData.Token is not null;
    //public bool CanAddToBookmarks
    //{
    //    get
    //    {
    //        var result = News is not null && FullNews is not null && AuthData.Token is not null;
    //        Debug.WriteLine($"{result} {News is not null} {FullNews is not null} {AuthData.Token is not null} - CanAddToBookmarks");
    //        return result;
    //    }
    //}
    [RelayCommand(CanExecute = nameof(CanAddToBookmarks))]
    private async Task AddToBookmarks()
    {
        if (!CanAddToBookmarks || IsFetching)
            return;

        // Сохраняем закладку
        if (!FullNews.IsBookmarked)
            await Auxiliary.RunWithStateHandling<FavouriteResponse?>(
                () => Fetcher.Favourite.Post(News.Id),
                null,
                _ => Error = _,
                r =>
                {
                    if (r is not null)
                        FullNews!.IsBookmarked = true;

                    OnPropertyChanged(nameof(FullNews));
                }
            );
        else
        {
            var response = await Auxiliary.RunWithStateHandling<FavouriteResponse?>(
                () => Fetcher.Favourite.Delete(News.Id),
                null,
                _ => Error = _,
                null
            );

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                FullNews!.IsBookmarked = false;
                OnPropertyChanged(nameof(FullNews));
            }
        }
    }

    [RelayCommand]
    private async Task OpenEditNews()
    {
        await Shell.Current.Navigation.PushAsync(new NewsEditPage(FullNews));
        await UpdateNews();
    }

    [RelayCommand]
    private async Task OpenCommentsPage()
    {
        await Shell.Current.Navigation.PushModalAsync(new CommentsPage(News.Id));
    }

    [RelayCommand]
    private async Task OpenCatalogPage()
    {
        try
        {
            await Shell.Current.GoToAsync("//Browse");
        } catch
        {

        }
    }

    public void ChangedAll()
    {
        OnPropertyChanged(nameof(ReadTime));

        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(120));
        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(250));
        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(550));
        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(750));
        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(1400));
        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(2800));
        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(5200));
        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(52000));
        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(520000));
        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(5200000));
        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(52000000));
        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(520000000));
        //Debug.WriteLine(Auxiliary.SymbolsToReadTime(999999999));
    }
}