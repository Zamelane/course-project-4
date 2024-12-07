using RequestsLibrary.Exceptions;
using RequestsLibrary.RequestsData;
using RequestsLibrary.Responses.Api.Auth;
using RequestsLibrary.Types;
using System.Diagnostics;

namespace RequestsLibrary.Routes.API.Auth
{
    public class LoginRequest : DefaultRoute
    {
        public LoginRequest(string login, string password)
            : base("login", HttpMethod.Post, null, null)
        {
            Body = new Content()
            {
                Value = new LoginRequestData(login, password),
                ValueType = Content.ContentType.JSON
            };
        }

        public async Task<(HttpResponseMessage, LoginResponse?, RequestException?)> RequestToServer()
        {
            return await CustomRequestToServer<LoginResponse>();
        }
    }
}
