using System.Collections.ObjectModel;
using RequestsLibrary.Exceptions;
using RequestsLibrary.Responses.Api.News;

namespace RequestsLibrary.Routes.API.News;

public static class FilteredNewsRequest
{
    public static async Task<(HttpResponseMessage, ObservableCollection<FilteredNewsResponse>?, RequestException?)>
        RequestToServer()
    {
        return await DefaultRoute.CustomRequestToServer<ObservableCollection<FilteredNewsResponse>>("news",
            HttpMethod.Get);
    }
}