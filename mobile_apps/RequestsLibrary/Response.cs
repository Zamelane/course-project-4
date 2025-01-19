using RequestsLibrary.Responses;
using System.Net;

namespace RequestsLibrary;
public class Response<T>
{
    public Error?   Error;
    public T?       Content;
    public HttpStatusCode StatusCode;
    public bool IsEmpty
    {
        get => IsEmptyContent && IsEmptyError;
    }
    public bool IsEmptyContent
    {
        get => Content is null;
    }
    public bool IsEmptyError
    {
        get => Error is null;
    }
}

public class Error
{
    private string? comment;
    public string? Comment
    {
        get => ValidationResponse?.Message ?? comment;
        set => comment = value;
    }
    public int? StatusCode;
    public ErrorResponse? ValidationResponse;
}
