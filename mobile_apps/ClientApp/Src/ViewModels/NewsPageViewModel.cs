using ClientApp.Src.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using RequestsLibrary;
using RequestsLibrary.Models;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;
public partial class NewsPageViewModel : ObservableObject
{
    [ObservableProperty] private MinNews? news;
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

    private void ChangedAll()
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