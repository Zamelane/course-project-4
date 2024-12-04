using RequestsLibrary.Exceptions;
using RequestsLibrary.Types;
using System;
using System.Collections.Generic;
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

        // TODO: Сделать обработку стандартных ошибок от сервера
        protected async Task<(HttpResponseMessage, T?, RequestException?)> CustomRequestToServer<T>()
        {
            return await Fetcher.SendCustomRequest<T?, RequestException>(Method, Path, Headers, Body);
        }
    }
}
