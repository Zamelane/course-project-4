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
        => Join($"{_protocol.ToString()}://{_host}", _path);

    public string GetApiUrl(string path = "")
        => Join(Join(GetServerUrl(), _apiPrefix), path);

    public string GetStorageUrl() 
        => Join(GetServerUrl(), "storage");

    private string Join(string s1, string? s2)
    {
        s1 = s1.Trim('/');
        s2 = s2?.Trim('/');

        if (String.IsNullOrEmpty(s2))
            return s1;

        return $"{s1}/{s2}";
    }
}
