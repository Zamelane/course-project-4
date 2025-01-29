using ClientApp.Src.Storage;
using ClientApp.Src.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using RequestsLibrary.Responses;
using System.Net;

namespace ClientApp.Src.ViewModels;
public partial class NewsPageViewModel : ObservableObject
{
    [ObservableProperty] private MinNews? news;
    [NotifyCanExecuteChangedFor(nameof(AddToBookmarksCommand))]
    [NotifyPropertyChangedFor(nameof(CanAddToBookmarks))]
    [ObservableProperty] private FullNews? fullNews;

    // Вычисляемые поля
    public string? FormattedDate
    {
        get => News is null ? null : Auxiliary.FormateDate(News.CreateDate);
    }
    public string? ReadTime
    {
        get => News is null ? null : Auxiliary.SymbolsToReadTime(News.Content!.Length);
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
                ChangedAll();
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
        && AuthData.Token is not null;
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

    public void ChangedAll()
    {
        OnPropertyChanged(nameof(FormattedDate));
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