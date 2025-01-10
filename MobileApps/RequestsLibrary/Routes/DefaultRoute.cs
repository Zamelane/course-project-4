using RequestsLibrary.Exceptions;
using RequestsLibrary.Responses.Api;
using RequestsLibrary.Types;

namespace RequestsLibrary.Routes;

public static class DefaultRoute
{
    public static async Task<(HttpResponseMessage, T?, RequestException?)> CustomRequestToServer<T>(
        string Path,
        HttpMethod Method,
        Content? Body = null,
        Headers[]? Headers = null
    )
    {
        try
        {
            var (response, body, responseException) =
                await Fetcher.SendCustomRequest<T?, ErrorResponse>(Method, Path, Headers, Body);
            RequestException? exception = null;
            if (responseException != null || !response.IsSuccessStatusCode)
            {
                var exceptionDetect = ExceptionsFormatter.Detect(response, null, responseException);
                if (exceptionDetect != null)
                    exception = new RequestException(exceptionDetect);
            }

            return (response, body, exception);
        }
        catch (Exception ex)
        {
            return (new HttpResponseMessage(), default,
                new RequestException(ExceptionsFormatter.Detect(null, ex) ?? "Ошибка запросника: см. DefaultRoute"));
        }
    }
}