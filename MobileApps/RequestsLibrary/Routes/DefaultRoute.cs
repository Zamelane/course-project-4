using RequestsLibrary.Exceptions;
using RequestsLibrary.Responses.Api;
using RequestsLibrary.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestsLibrary.Routes
{
    public class DefaultRoute
    {
        public string Path;
        public Content? Body;
        public HttpMethod Method;
        public Headers[]? Headers;

        public DefaultRoute(string path, HttpMethod method, Content? body = null, Headers[]? headers = null)
        {
            Path = path;
            Body = body;
            Method = method;
            Headers = headers;
        }

        protected async Task<(HttpResponseMessage, T?, RequestException?)> CustomRequestToServer<T>()
        {
            try
            {
                (var response, var body, var responseException) = await Fetcher.SendCustomRequest<T?, ErrorResponse>(Method, Path, Headers, Body);
                RequestException? exception = null;
                if (responseException != null || !response.IsSuccessStatusCode)
                {
                    var exceptionDetect = ExceptionsFormatter.Detect(response, null, responseException as ErrorResponse);
                    if (exceptionDetect != null)
                        exception = new RequestException(exceptionDetect);
                }
                return (response, body, exception);
            }
            catch (Exception ex)
            {
                return (new HttpResponseMessage(), default, new RequestException(ExceptionsFormatter.Detect(null, ex) ?? "Ошибка запросника: см. DefaultRoute"));
            }
        }
    }
}
