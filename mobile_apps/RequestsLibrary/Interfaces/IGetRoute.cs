using System.Collections.ObjectModel;

namespace RequestsLibrary.Interfaces;
public interface IGetRoute<T, R>
{
    public Task<Response<ObservableCollection<T>?>> Get(RequestParams? rp = null);
    public Task<Response<R?>> Get(int id, RequestParams? rp = null);
    public Task<Response<T?>> Get(string id, RequestParams? rp = null);
}