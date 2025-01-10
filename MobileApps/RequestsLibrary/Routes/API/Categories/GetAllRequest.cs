using System.Collections.ObjectModel;
using RequestsLibrary.Exceptions;
using RequestsLibrary.Responses.Api.Category;

namespace RequestsLibrary.Routes.API.Categories;

public class GetAllRequest
{
    public static async Task<(HttpResponseMessage, ObservableCollection<CategoryResponse>?, RequestException?)>
        RequestToServer()
    {
        return await DefaultRoute.CustomRequestToServer<ObservableCollection<CategoryResponse>>("categories",
            HttpMethod.Get);
    }
}