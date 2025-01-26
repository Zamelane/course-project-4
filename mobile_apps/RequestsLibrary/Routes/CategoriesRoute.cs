using RequestsLibrary.Interfaces;
using RequestsLibrary.Models;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Routes;
public class CategoriesRoute : IGetRoute<Category, Category>
{
    public async Task<Response<ObservableCollection<Category>?>> Get(RequestParams? rp = null)
    {
        return await Fetcher.Fetch<ObservableCollection<Category>>(
           HttpMethod.Get,
           Fetcher.Config.GetApiUrl("categories")
       );
    }

    public Task<Response<Category?>> Get(int id, RequestParams? rp = null)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Category?>> Get(string id, RequestParams? rp = null)
    {
        throw new NotImplementedException();
    }
}