using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;

public partial class CategoriesViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Category> categories = [];
    [ObservableProperty] private string? error;
    [ObservableProperty] private bool isFetching;

    public CategoriesViewModel()
    {
        Task.Run(TryFetchCategories);
    }

    [RelayCommand]
    private async Task TryFetchCategories()
    {
        await Fetch();
    }

    public async Task<ObservableCollection<Category>?> Fetch()
    {
        try
        {
            IsFetching = true;
            Debug.WriteLine("Start TryFetchCategories"); // TODO: DEBUG
            var response = await Fetcher.Categories.Get();

            if (response.Content is null && response.Error is null)
                Error = "Сервер не вернул данные: "/* +
                                                 response.StatusCode*/;

            if (response.Error is not null)
            {
                Error = response.Error.Comment;
                Debug.WriteLine("Error TryFetchNews: " + Error); // TODO: DEBUG
                return null;
            }

            foreach (var cr in response.Content!)
            {
                //cr.AccentColor = $"#{cr.AccentColor}";
                cr.BackgroundColor = $"#{cr.BackgroundColor}";
                if (cr.Image is not null)
                    cr.Image.Path = $"{Fetcher.Config.GetServerUrl()}/{cr.Image.Path}";
                Debug.WriteLine(cr.BackgroundColor);
            }

            Categories = response.Content;

            Debug.WriteLine("End TryFetchCategories"); // TODO: DEBUG
        }
        finally
        {
            IsFetching = false;
        }

        return Categories;
    }
}