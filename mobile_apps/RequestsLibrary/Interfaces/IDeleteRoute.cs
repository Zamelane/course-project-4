namespace RequestsLibrary.Interfaces;
public interface IDeleteRoute<T>
{
    public Task<Response<T?>> Delete(int id, RequestParams? rp = null);
    public Task<Response<T?>> Delete(string id, RequestParams? rp = null);
}