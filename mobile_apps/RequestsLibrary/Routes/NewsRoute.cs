using RequestsLibrary.Interfaces;
using RequestsLibrary.Models;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Routes;
public class NewsRoute : IGetRoute<News>
{
    public async Task<Response<ObservableCollection<News>?>> Get(RequestParams? rp = null)
    {
        return await Fetcher.Fetch<ObservableCollection<News>>(
            HttpMethod.Get,
            Fetcher.Config.GetApiUrl("news")
        );
    }

    public Task<Response<News?>> Get(int id, RequestParams? rp = null)
    {
        throw new NotImplementedException();
    }

    public Task<Response<News?>> Get(string id, RequestParams? rp = null)
    {
        throw new NotImplementedException();
    }
}
