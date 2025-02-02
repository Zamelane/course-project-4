using RequestsLibrary.Interfaces;
using RequestsLibrary.Models;
using RequestsLibrary.Requests;
using RequestsLibrary.Responses;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Routes;
public class FavouriteRoute : IGetRoute<FavouriteResponse, FullNews>, IPostRoute<FavouriteResponse>, IDeleteRoute<FavouriteResponse>
{

    public Task<Response<FavouriteResponse?>> Post(RequestParams? rp = null)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<FavouriteResponse?>> Post(int id, RequestParams? rp = null)
    {
        rp ??= new()
        {
            Body = new FavouriteRequest(id)
        };

        return await Fetcher.Fetch<FavouriteResponse>(
           HttpMethod.Post,
           Fetcher.Config.GetApiUrl("favourites"),
           rp
        );
    }

    public Task<Response<FavouriteResponse?>> Post(string id, RequestParams? rp = null)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<FavouriteResponse?>> Delete(int id, RequestParams? rp = null)
    {
        return await Fetcher.Fetch<FavouriteResponse>(
           HttpMethod.Delete,
           Fetcher.Config.GetApiUrl("favourites/" + id),
           rp
        );
    }

    public Task<Response<FavouriteResponse?>> Delete(string id, RequestParams? rp =null)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<FavouriteResponse?>> Get(RequestParams? rp = null)
    {
        return await Fetcher.Fetch<FavouriteResponse?> (
           HttpMethod.Get,
           Fetcher.Config.GetApiUrl("favourites"),
           rp
        );
    }

    Task<Response<ObservableCollection<FavouriteResponse>?>> IGetRoute<FavouriteResponse, FullNews>.Get(RequestParams? rp)
    {
        throw new NotImplementedException();
    }

    Task<Response<FullNews?>> IGetRoute<FavouriteResponse, FullNews>.Get(int id, RequestParams? rp)
    {
        throw new NotImplementedException();
    }

    Task<Response<FavouriteResponse?>> IGetRoute<FavouriteResponse, FullNews>.Get(string id, RequestParams? rp)
    {
        throw new NotImplementedException();
    }
}