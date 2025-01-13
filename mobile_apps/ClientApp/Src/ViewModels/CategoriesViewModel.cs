using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary;
using RequestsLibrary.Exceptions;
using RequestsLibrary.Responses.Api.Category;
using RequestsLibrary.Routes.API.Categories;

namespace ClientApp.Src.ViewModels;

public partial class CategoriesViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<CategoryResponse> categories = new();
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

    public async Task<ObservableCollection<CategoryResponse>?> Fetch()
    {
        try
        {
            IsFetching = true;
            Debug.WriteLine("Start TryFetchCategories"); // TODO: DEBUG
            var (response, body, exception) =
                await GetAllRequest.RequestToServer();

            if (body is null && exception is null)
                exception = new RequestException("Сервер не вернул данные: " +
                                                 response.StatusCode);

            if (exception is not null)
            {
                Error = exception.Message;
                Debug.WriteLine("Error TryFetchCategories: " + Error); // TODO: DEBUG
                return null;
            }

            foreach (var cr in body!)
            {
                cr.AccentColor = $"#{cr.AccentColor}";
                cr.BackgroundColor = $"#{cr.BackgroundColor}";
                if (cr.Image is not null)
                    cr.Image.Path = $"{Fetcher.URL}{cr.Image.Path}";
            }

            Categories = body;

            Debug.WriteLine("End TryFetchCategories"); // TODO: DEBUG
        }
        finally
        {
            IsFetching = false;
        }

        return Categories;
    }
}