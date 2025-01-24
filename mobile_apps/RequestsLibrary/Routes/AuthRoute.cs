using RequestsLibrary.Interfaces;
using RequestsLibrary.Requests;
using RequestsLibrary.Responses;

namespace RequestsLibrary.Routes;
public class AuthRoute : IPostRoute<AuthResponse>
{
    public async Task<Response<AuthResponse?>> Login(string login, string password)
    {
        var rp = new RequestParams();
        rp.SetBody(new AuthRequest(login, password));

        return await Post(rp);
    }

    public async Task<Response<AuthResponse?>> Register(string login, string password, string firstName, string lastName, DateTime birthDay, string email)
    {
        var rp = new RequestParams();
        rp.SetBody(new RegisterRequest(login, password, firstName, lastName, birthDay, email));

        return await Fetcher.Fetch<AuthResponse>(
           HttpMethod.Post,
           Fetcher.Config.GetApiUrl("register"),
           rp
        );
    }

    public async Task<Response<AuthResponse?>> Logout()
    {
        return await Fetcher.Fetch<AuthResponse>(
           HttpMethod.Get,
           Fetcher.Config.GetApiUrl("logout")
        );
    }

    public async Task<Response<AuthResponse?>> Post(RequestParams? rp = null)
    {
        return await Fetcher.Fetch<AuthResponse>(
           HttpMethod.Post,
           Fetcher.Config.GetApiUrl("login"),
           rp
        );
    }

    public Task<Response<AuthResponse?>> Post(int id, RequestParams? rp = null)
    {
        throw new NotImplementedException();
    }

    public Task<Response<AuthResponse?>> Post(string id, RequestParams? rp = null)
    {
        throw new NotImplementedException();
    }
}