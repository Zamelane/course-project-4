using RequestsLibrary.Responses.Api.News;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Routes;
public class News : IGetRoute<FilteredNewsResponse>
{
    public Task<Response<ObservableCollection<FilteredNewsResponse>?>> Get()
    {
        return Fetcher.Fetch<ObservableCollection<FilteredNewsResponse>>(
            HttpMethod.Get,
            Fetcher.Config.GetApiUrl("news")
        );
    }

    public Task<FilteredNewsResponse?> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<FilteredNewsResponse?> Get(string id)
    {
        throw new NotImplementedException();
    }
}
