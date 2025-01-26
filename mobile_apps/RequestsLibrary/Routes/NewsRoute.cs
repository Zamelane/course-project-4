using RequestsLibrary.Interfaces;
using RequestsLibrary.Models;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Routes;
public class NewsRoute : IGetRoute<MinNews, FullNews>
{
    public async Task<Response<ObservableCollection<MinNews>?>> Get(RequestParams? rp = null)
    {
        return await Fetcher.Fetch<ObservableCollection<MinNews>>(
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

    public Task<Response<MinNews?>> Get(string id, RequestParams? rp = null)
    {
        throw new NotImplementedException();
    }
}
