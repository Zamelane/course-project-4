using RequestsLibrary.Exceptions;
using RequestsLibrary.RequestsData;
using RequestsLibrary.Responses.Api.Auth;
using RequestsLibrary.Types;
using System.Diagnostics;

namespace RequestsLibrary.Routes.API.Auth
{
    public static class LoginRequest
    {
        public static async Task<(HttpResponseMessage, LoginResponse?, RequestException?)> RequestToServer(
            string login,
            string password
        )
        {
            var Body = new Content()
            {
                Value = new LoginRequestData(login, password),
                ValueType = Content.ContentType.JSON
            };
            return await DefaultRoute.CustomRequestToServer<LoginResponse>(Path: "login", Method: HttpMethod.Post, Body: Body);
        }
    }
}
