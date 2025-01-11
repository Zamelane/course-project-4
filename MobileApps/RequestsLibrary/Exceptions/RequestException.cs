namespace RequestsLibrary.Exceptions;

public class RequestException : Exception
{
    public RequestException(string message, int responseCode = -1) : base(message)
    {
        Message = message;
        ResponseCode = responseCode;
    }

    public override string Message { get; }
    public int? ResponseCode { get; }
}