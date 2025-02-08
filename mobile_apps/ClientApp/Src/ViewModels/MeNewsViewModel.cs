using ClientApp.Src.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary.Models;
using RequestsLibrary;
using System.Collections.ObjectModel;
using System.Diagnostics;
using RequestsLibrary.Responses;
using ClientApp.Src.Views;

namespace ClientApp.Src.ViewModels;
public partial class MeNewsViewModel : ObservableObject
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
            string title = "Me news";

            if (!News.Any())
                return title;

            return title + $" ({News.Count})";
        }
    }

    public MeNewsViewModel()
    {
        GetMoreNewsCommand.Execute(null);
    }
    public bool CanGetMoreNews => !IsEndPage;
    [RelayCommand(CanExecute = nameof(CanGetMoreNews))]
    private async Task GetMoreNews()
    {
        if (IsEndPage || IsFetching)
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
            async () => await Fetcher.Fetch<NewsResponse>(
                HttpMethod.Get,
                Fetcher.Config.GetApiUrl("news"),
                rp
            ),
            _ => IsFetching = _,
            _ => Error = _,
            r =>
            {
                if (r is null)
                {
                    Debug.WriteLine("Ничего не пришло для своих новостей");
                    page--;
                    return;
                }

                if (r.News.Count == 0)
                {
                    Debug.WriteLine("Свои новости пустые");
                    IsEndPage = true;
                }
                else
                    r.News.ToList().ForEach(e => News.Add(e));
                Debug.WriteLine($"Свои новости: {News.Count}");

                Total = r.Total;

                OnPropertyChanged(nameof(Title));
            }
        );
    }

    [RelayCommand]
    private void UpdateNewsList()
    {
        page = 0;
        GetMoreNewsCommand.Execute(null);
    }

    [RelayCommand]
    private void OpenNewsEditor()
    {
        Shell.Current.Navigation.PushAsync(new NewsEditPage());
    }
}