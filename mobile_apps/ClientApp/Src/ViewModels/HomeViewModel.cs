using ClientApp.Src.Storage;
using ClientApp.Src.Utils;
using ClientApp.Src.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using RequestsLibrary.Responses;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly string _email = "test@mail.ru";
    [ObservableProperty] private ObservableCollection<Category>? categories;
    [ObservableProperty] private string? error;
    [ObservableProperty] private bool isFetching;

    [ObservableProperty] private ObservableCollection<MinNews> topNews = [];
    [ObservableProperty] private ObservableCollection<MinNews> lastNews = [];
    [ObservableProperty] private ObservableCollection<MinNews> randomNews = [];
    [ObservableProperty] private User? user = Provider.AuthData.User;
    [ObservableProperty] private string email;
    [ObservableProperty] private bool isAuthorized = false;

    private void AuthData_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AuthData.User))
        {
            // Обновляем свойство User при изменении AuthData.User
            User = Provider.AuthData.User;

            if (User is not null)
                Email = User.Email;
            else Email = _email;

            IsAuthorized = User is not null;

            OnPropertyChanged(nameof(User));
        }
    }

    public HomeViewModel()
    {
        Task.Run(TryFetch);
        Provider.AuthData.PropertyChanged += AuthData_PropertyChanged;
        IsAuthorized = User is not null;
        Debug.WriteLine("User avatar: " + User?.Avatar?.TotalPath);
        Email = _email;
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

        IsFetching = true;

        RequestParams rpLAST = new();
        rpLAST.AddParameter("limit", "5");

        RequestParams rpTOP = new();
        rpTOP.AddParameter("sort", "FirstMoreViews");
        rpTOP.AddParameter("limit", "5");

        RequestParams rpRANDOM = new();
        rpRANDOM.AddParameter("sort", "Random");
        rpRANDOM.AddParameter("limit", "5");

        await Auxiliary.RunWithStateHandling<NewsResponse>(
            () => Fetcher.News.Get(rpLAST),
            null,
            _ => Error = _,
            newses =>
            {
                if (newses is not null && newses!.News.Count > 0)
                {
                    LastNews = newses.News;
                }
            }
        );

        await Auxiliary.RunWithStateHandling<NewsResponse>(
            () => Fetcher.News.Get(rpTOP),
            null,
            _ => Error = _,
            newses =>
            {
                if (newses is not null && newses!.News.Count > 0)
                {
                    TopNews = newses.News;
                    Debug.WriteLine("First: " + TopNews.First().Id);
                }
            }
        );

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

        IsFetching = false;

        Debug.WriteLine("End TryFetchNews"); // TODO: DEBUG
    }

    [RelayCommand]
    private static void ShowCategoriesPage() => Shell.Current.Navigation.PushAsync(new CategoriesPage());

    [RelayCommand]
    private static async Task OpenProfileEditorPage()
    {
        if (Provider.AuthData.Token is not null)
            await Shell.Current.Navigation.PushAsync(new ProfilePage());
        else await Toast.Make("Вы не авторизованы").Show();
    }
}