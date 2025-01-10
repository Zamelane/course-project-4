using RequestsLibrary.Exceptions;
using RequestsLibrary.RequestsData;
using RequestsLibrary.Responses.Api.Auth;
using RequestsLibrary.Types;

namespace RequestsLibrary.Routes.API.Auth;

public static class SignupRequest
{
    public static async Task<(HttpResponseMessage, SignupResponse?, RequestException?)> RequestToServer(
        string firstName,
        string lastName,
        string login,
        string password,
        DateTime birthDay,
        string email
    )
    {
        var Body = new Content
        {
            Value = new SignupRequestData(firstName, lastName, login, password, birthDay, email),
            ValueType = Content.ContentType.JSON
        };
        return await DefaultRoute.CustomRequestToServer<SignupResponse>("register", HttpMethod.Post, Body);
    }
}