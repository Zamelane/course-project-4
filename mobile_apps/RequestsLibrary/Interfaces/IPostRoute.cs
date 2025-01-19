namespace RequestsLibrary.Interfaces;
public interface IPostRoute<T>
{
    public Task<Response<T?>> Post(RequestParams? rp = null);
    public Task<Response<T?>> Post(int id, RequestParams? rp = null);
    public Task<Response<T?>> Post(string id, RequestParams? rp = null);
}