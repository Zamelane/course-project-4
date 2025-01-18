using System.Collections.ObjectModel;

namespace RequestsLibrary;
public interface IGetRoute<T>
{
    public Task<Response<ObservableCollection<T>?>> Get();
    public Task<T?> Get(int id);
    public Task<T?> Get(string id);
}