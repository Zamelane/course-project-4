using RequestsLibrary.Exceptions;
using RequestsLibrary.RequestsData;
using RequestsLibrary.Responses.Api.Auth;
using RequestsLibrary.Types;

namespace RequestsLibrary.Routes.API.Auth;
public static class LogoutRequest
{
    public static async Task<(HttpResponseMessage, dynamic?, RequestException?)> RequestToServer()
    {
        var Headers = new Headers[] {
            new Headers()
            {
                HeaderName = "Authorization",
                HeaderValue = "Bearer " + Fetcher.GetToken()
            }
        };
        return await DefaultRoute.CustomRequestToServer<dynamic>(Path: "logout", Method: HttpMethod.Post, Headers: Headers);
    }
}