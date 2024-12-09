using RequestsLibrary.Exceptions;
using RequestsLibrary.Responses.Api.News;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Routes.API.News;
public static class FilteredNewsRequest
{
    public static async Task<(HttpResponseMessage, ObservableCollection<FilteredNewsResponse>?, RequestException?)> RequestToServer()
    {
        return await DefaultRoute.CustomRequestToServer<ObservableCollection<FilteredNewsResponse>>(Path: "news", Method: HttpMethod.Get);
    }
}