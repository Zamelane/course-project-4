using System.Diagnostics;
using System.Text.Json;
using System.Web;

namespace RequestsLibrary;
public class RequestParams
{
    private Dictionary<string, string?> _params = [];
    private dynamic? _content = null;
    private Dictionary<string, string> _headers = [];
    public RequestParams() { }
    public void AddParameter(string key, string? value) => _params.Add(key, value);
    public void AddParameter(string key, int? value) => AddParameter(key, value.ToString());
    public void SetBody(dynamic content) => _content = content;
    public void AddHeader(string key, string value) => _headers.Add(key, value);

    public void AddToRequest(HttpRequestMessage request)
    {
        if (request.Method == HttpMethod.Get && _params.Any())
        {
            Debug.WriteLine("->>Добавляю GET парамерты в запрос");
            string getParams = String.Empty;

            foreach (KeyValuePair<string, string?> param in _params)
            {
                // TODO: Это бы как-то покрасивее (типа чтобы '?' только перед первым элементом был)
                string pairValue = String.Join('=', param.Key, HttpUtility.UrlEncode(param.Value));
                if (String.IsNullOrEmpty(getParams))
                    getParams = pairValue;
                else getParams = String.Join('&', getParams, pairValue);
            }

            Debug.WriteLine(getParams);
            request.RequestUri = new Uri(String.Join('?', request.RequestUri!.ToString(), getParams));

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