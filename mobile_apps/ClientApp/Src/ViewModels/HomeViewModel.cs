using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using ClientApp.Src.Storage;
using ClientApp.Src.Utils;
using ClientApp.Src.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;

namespace ClientApp.Src.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private string _email = "test@mail.ru";
    [ObservableProperty] private ObservableCollection<Category>? categories;
    [ObservableProperty] private string? error;
    [ObservableProperty] private bool isFetching;

    [ObservableProperty] private ObservableCollection<MinNews>? mostReadNewses;
    [ObservableProperty] private MinNews? mostReadNewsTop;
    [ObservableProperty] private User? user = Provider.AuthData.User;
    [ObservableProperty] private string email;

    private void AuthData_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AuthData.User))
        {
            // Обновляем свойство User при изменении AuthData.User
            User = Provider.AuthData.User;

            if (User is not null)
                Email = User.Email;
            else Email = _email;

            OnPropertyChanged(nameof(User));
        }
    }

    public HomeViewModel()
    {
        Task.Run(TryFetch);
        Provider.AuthData.PropertyChanged += AuthData_PropertyChanged;
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