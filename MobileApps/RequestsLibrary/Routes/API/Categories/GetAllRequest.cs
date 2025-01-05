using RequestsLibrary.Exceptions;
using RequestsLibrary.RequestsData;
using RequestsLibrary.Responses.Api.Auth;
using RequestsLibrary.Responses.Api.Category;
using RequestsLibrary.Types;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Routes.API.Categories
{
    public class GetAllRequest
    {
        public static async Task<(HttpResponseMessage, ObservableCollection<CategoryResponse>?, RequestException?)> RequestToServer()
        {
            return await DefaultRoute.CustomRequestToServer<ObservableCollection<CategoryResponse>>(Path: "categories", Method: HttpMethod.Get);
        }
    }
}
