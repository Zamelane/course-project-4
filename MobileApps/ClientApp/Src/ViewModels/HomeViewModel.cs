using ClientApp.Src.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    private ObservableCollection<FilteredNewsResponse>? mostReadNewses = null;
    [ObservableProperty]
    private FilteredNewsResponse? mostReadNewsTop = null;

    public HomeViewModel()
    {
        TryFetchNews();
    }

    [RelayCommand]
    private async Task TryFetchNews()
    {
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

        if (body.Count > 0)
        {
            MostReadNewsTop = body.First();
            body.Remove(MostReadNewsTop);
            MostReadNewses = body;
        }

        Debug.WriteLine("End TryFetchNews"); // TODO: DEBUG
    }
}