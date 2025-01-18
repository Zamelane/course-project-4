namespace RequestsLibrary;
public class Response<T>
{
    public Error?   Error;
    public T?       Content;
}

public class Error
{
    public string? comment;
}
