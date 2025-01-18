namespace RequestsLibrary;
public class Config
{
    private Protocol _protocol;
    private string   _host;
    private string   _path;
    private string   _apiPrefix;
    public enum Protocol
    {
        http,
        https
    }

    public Config(Protocol protocol, string host, string path = "", string apiPrefix = "api")
    {
        _protocol = protocol;
        _host = host;
        _path = path;
        _apiPrefix = apiPrefix;
    }

    public string GetServerUrl() 
        => String.Join("/", $"{_protocol.ToString()}://{_host}", _path.Trim('/'));

    public string GetApiUrl(string path = "") 
        => String.Join("/", GetServerUrl(), _apiPrefix.Trim('/'), path.Trim('/'));

    public string GetStorageUrl() 
        => String.Join("/", GetServerUrl(), "storage");
}
