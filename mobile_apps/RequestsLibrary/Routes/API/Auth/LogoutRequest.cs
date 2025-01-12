using RequestsLibrary.Exceptions;
using RequestsLibrary.Types;

namespace RequestsLibrary.Routes.API.Auth;

public static class LogoutRequest
{
    public static async Task<(HttpResponseMessage, dynamic?, RequestException?)> RequestToServer()
    {
        var Headers = new[]
        {
            new Headers
            {
                HeaderName = "Authorization",
                HeaderValue = "Bearer " + Fetcher.GetToken()
            }
        };
        return await DefaultRoute.CustomRequestToServer<dynamic>("logout", HttpMethod.Post, Headers: Headers);
    }
}