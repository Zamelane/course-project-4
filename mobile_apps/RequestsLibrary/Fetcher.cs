using System.Text.Json;
using RequestsLibrary.Routes;
using RequestsLibrary.Types;

namespace RequestsLibrary;

public static class Fetcher
{
    public static Config Config = new(Config.Protocol.http, "127.0.0.1");

    private static string? _token;

    private static readonly HttpClient _httpClient = new();

    public static void SetToken(string? token)
    {
        _token = token;
    }

    internal static string? GetToken()
    {
        return _token;
    }

    public static async Task<Response<T?>> Fetch<T>(
        HttpMethod method,
        string url,
        RequestParams? rp = null
    )
    {
        // Инициализируем метод запроса
        var request = new HttpRequestMessage(method, url);

        if (rp is null)
            rp = new RequestParams();

        if (_token is not null)
            rp.AddHeader("Authorization", $"Bearer {_token}");

        // Добавляем нагрузку в запрос
        rp?.AddToRequest(request);

        // Выполняем запрос
        var response = await _httpClient.SendAsync(request);

        // Читаем ответ и отдаём ответ
        string? responseContent = null;
        string? error = null;
        T? content = default(T);
        try
        {
            responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                error = response.StatusCode.ToString();

            content = JsonSerializer.Deserialize<T>(responseContent);

        }
        catch (Exception) when (!response.IsSuccessStatusCode)
        {
            error = response.StatusCode.ToString();
        }

        return new Response<T?>()
        {
            Content = content,
            Error = error is not null ? new()
            {
                comment = error
            }
            : null
        };
    }

    public static News News = new();
}