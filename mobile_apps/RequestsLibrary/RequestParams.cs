using System.Diagnostics;
using System.Text.Json;

namespace RequestsLibrary;
public class RequestParams
{
    private List<string> _params = [];
    private dynamic? _content = null;
    private Dictionary<string, string> _headers = [];
    public RequestParams() { }
    public void AddParameter(string key, string? value) => _params.Add($"{key}={value}");
    public void AddParameter(string key, int? value) => AddParameter(key, value.ToString());
    public void SetBody(dynamic content) => _content = content;
    public void AddHeader(string key, string value) => _headers.Add(key, value);

    public void AddToRequest(HttpRequestMessage request)
    {
        if (request.Method == HttpMethod.Get && _params.Any())
        {
            Debug.WriteLine("->>Добавляю GET парамерты в запрос");
            string getParams = String.Join("&", _params);

            request.RequestUri = new ($"{request.RequestUri?.ToString()}?{getParams}");
        }

        if (_headers.Any())
            foreach (KeyValuePair<string, string> param in _headers)
                request.Headers.Add(param.Key, param.Value);

        if (_content is HttpContent)
        {
            request.Content = _content;
        }
        else if (_content is not null)
        {
            string json = JsonSerializer.Serialize(_content);
            Debug.WriteLine("FETCH: REQUEST: JSON: " + json);
            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        }
    }
}