using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestsLibrary.Types
{
    public class Headers
    {
        public string? HeaderName { get; set; }
        public string? HeaderValue { get; set; }

        public void AddToRequest(HttpRequestMessage request)
        {
            if (HeaderName != null && HeaderValue != null)
                request.Headers.Add(HeaderName, HeaderValue);
        }
    }
}
