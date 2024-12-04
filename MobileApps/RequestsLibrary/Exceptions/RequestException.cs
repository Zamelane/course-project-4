using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestsLibrary.Exceptions
{
    public class RequestException : Exception
    {
        public override string Message { get; }
        public int?   ResponseCode { get; }

        public RequestException(string message, int responseCode = -1) : base(message)
        {
            Message = message;
            ResponseCode = responseCode;
        }
    }
}
