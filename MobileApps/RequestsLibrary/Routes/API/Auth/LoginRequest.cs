using RequestsLibrary.Exceptions;
using RequestsLibrary.Responses.Api.Auth;
using RequestsLibrary.Types;

namespace RequestsLibrary.Routes.API.Auth
{
    public class LoginRequest : DefaultRoute
    {
        public LoginRequest(string login, string password)
            : base("login", HttpMethod.Get, null, null)
        {
            Body = new Content()
            {
                Value = new LoginRequest(login, password)
            };
        }

        public async Task<(HttpResponseMessage, LoginResponse?, RequestException?)> RequestToServer()
        {
            return await CustomRequestToServer<LoginResponse>();
        }
    }
}
