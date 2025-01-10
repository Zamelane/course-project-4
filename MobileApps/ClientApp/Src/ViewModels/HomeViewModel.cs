using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RequestsLibrary.Exceptions;
using RequestsLibrary.Responses.Api.Category;
using RequestsLibrary.Responses.Api.News;
using RequestsLibrary.Routes.API.News;

namespace ClientApp.Src.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<CategoryResponse>? categories;
    [ObservableProperty] private string? error;
    [ObservableProperty] private bool isFetching;

    [ObservableProperty] private ObservableCollection<FilteredNewsResponse>? mostReadNewses;
    [ObservableProperty] private FilteredNewsResponse? mostReadNewsTop;

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
            var (response, body, exception) = await FilteredNewsRequest.RequestToServer();

            if (body is null && exception is null)
                exception = new RequestException("Сервер не вернул данные: " +
                                                 response.StatusCode);

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