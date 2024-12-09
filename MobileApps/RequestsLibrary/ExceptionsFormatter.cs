using RequestsLibrary.Responses.Api;
using System.Diagnostics;

namespace RequestsLibrary;
public static class ExceptionsFormatter
{
    public static string? Detect(HttpResponseMessage? response, Exception? ex = null, ErrorResponse? body = null)
    {
        if (ex is not null)
            return DetectThrownException(ex);

        if (response is not null)
        {
            if (body?.Message is not null)
                return DetectServerErrors(body);

            Debug.WriteLine(body?.Message ?? "Fdsfasfdasf");

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                    return "Неправильный логин и/или пароль";
            }
        }

        if (response == null)
            return "Неопознанная ошибка";

        return null;
    }

    private static string DetectServerErrors(ErrorResponse body)
    {
        switch (body.Message)
        {
            case "Invalid credentials":
                return "Неправильный логин и/или пароль";
            case "Validation failed":
                return "Ошибка валидации полей";
            default:
                return body.Message;
        }
    }

    private static string DetectThrownException(Exception ex)
    {
        if (ex is HttpRequestException)
            return "Не удалось сделать запрос к серверу";

        return "Неопознанная ошибка в приложении";
    }
}