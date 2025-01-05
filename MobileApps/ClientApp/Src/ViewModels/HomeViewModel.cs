using ClientApp.Src.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary.Responses.Api.Category;
using RequestsLibrary.Responses.Api.News;
using RequestsLibrary.Routes.API.News;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ClientApp.Src.ViewModels;
public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty]
    private string? error = null;
    [ObservableProperty]
    private bool isFetching = false;

    [ObservableProperty]
    private ObservableCollection<FilteredNewsResponse>? mostReadNewses = null;
    [ObservableProperty]
    private FilteredNewsResponse? mostReadNewsTop = null;
    [ObservableProperty]
    private ObservableCollection<CategoryResponse>? categories = null;

    public HomeViewModel()
    {
        Task.Run(TryFetch);
    }

    [RelayCommand]
    private async Task TryFetch()
    {
        await TryFetchNews();
        Categories = await new CategoriesViewModel().Fetch();
        Debug.WriteLine(Categories);
    }
    private async Task TryFetchNews()
    {
        try
        {
            IsFetching = true;
            Debug.WriteLine("Start TryFetchNews"); // TODO: DEBUG
            (var response, var body, var exception) = await FilteredNewsRequest.RequestToServer();

            if (body is null && exception is null)
                exception = new RequestsLibrary.Exceptions.RequestException("Сервер не вернул данные: " + response.StatusCode);

            if (exception is not null)
            {
                Error = exception.Message;
                Debug.WriteLine("Error TryFetchNews: " + Error); // TODO: DEBUG
                return;
            }

            if (body!.Count > 0)
            {
                MostReadNewsTop = body.First();
                body.Remove(MostReadNewsTop);
                MostReadNewses = body;
            }

            Debug.WriteLine("End TryFetchNews"); // TODO: DEBUG
        }
        finally
        {
            IsFetching = false;
        }
    }
}