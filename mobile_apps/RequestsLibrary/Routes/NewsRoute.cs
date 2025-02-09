using RequestsLibrary.Interfaces;
using RequestsLibrary.Models;
using RequestsLibrary.Responses;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Routes;
public class NewsRoute : IGetRoute<NewsResponse, FullNews>
{
    public async Task<Response<NewsResponse?>> Get(RequestParams? rp = null)
    {
        return await Fetcher.Fetch<NewsResponse>(
            HttpMethod.Get,
            Fetcher.Config.GetApiUrl("news"),
            rp
        );
    }

    public async Task<Response<FullNews?>> Get(int id, RequestParams? rp = null)
    {
        return await Fetcher.Fetch<FullNews>(
            HttpMethod.Get,
            Fetcher.Config.GetApiUrl("news/" + id),
            rp
        );
    }

    public Task<Response<NewsResponse?>> Get(string id, RequestParams? rp = null)
    {
        throw new NotImplementedException();
    }

    Task<Response<ObservableCollection<NewsResponse>?>> IGetRoute<NewsResponse, FullNews>.Get(RequestParams? rp)
    {
        throw new NotImplementedException();
    }
}
