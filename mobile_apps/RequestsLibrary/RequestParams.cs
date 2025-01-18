namespace RequestsLibrary;
public class RequestParams
{
    private Dictionary<string, string?> _params  = [];
    private dynamic?                    _content = null;
    private Dictionary<string, string>  _headers = [];
    public RequestParams() { }
    public void AddParameter(string key, string? value) => _params.Add(key, value);
    public void AddParameter(string key, int? value)    => AddParameter(key, value.ToString());
    public void SetBody(dynamic content)               => _content = content;
    public void AddHeader(string key, string value)    => _headers.Add(key, value);

    public void AddToRequest(HttpRequestMessage request)
    {
        if (request.Method == HttpMethod.Get)
        {
            string getParams = String.Empty;

            
            foreach (KeyValuePair<string, string?> param in _params)
            {
                getParams = String.Join('&', getParams, String.Join('=', param.Key, param.Value));
            }

            request.RequestUri = new(String.Join('?', request.RequestUri!.ToString(), getParams));
        }

        if (_headers.Any())
            foreach (KeyValuePair<string, string> param in _headers)
                request.Headers.Add(param.Key, param.Value);

                if (_content is not null)
            request.Content = _content;
    }
}