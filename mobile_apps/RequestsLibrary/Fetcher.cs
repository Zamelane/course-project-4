using System.Diagnostics;
using System.Text.Json;
using RequestsLibrary.Responses;
using RequestsLibrary.Routes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RequestsLibrary;

public static class Fetcher
{
    public static Config Config = new(Config.Protocol.http, "127.0.0.1:8000");

    private static string? _token;

    private static readonly HttpClient _httpClient = new();

    public static void SetToken(string? token)
    {
        _token = token;
    }

    public static async Task<Response<T?>> Fetch<T>(
        HttpMethod method,
        string url,
        RequestParams? rp = null
    )
    {
        // Инициализируем метод запроса
        var request = new HttpRequestMessage(method, url);
        Debug.WriteLine(Config.GetApiUrl());
        if (rp is null)
            rp = new RequestParams();

        if (_token is not null)
            rp.AddHeader("Authorization", $"Bearer {_token}");

        // Добавляем нагрузку в запрос
        rp?.AddToRequest(request);

        Debug.WriteLine(_token);
        Debug.WriteLine(JsonSerializer.Serialize(request.Headers));

        Debug.WriteLine(request.RequestUri);

        // Выполняем запрос
        var response = await _httpClient.SendAsync(request);

        // Читаем ответ и отдаём ответ
        string? responseContent = null;
        Error? error = null;
        T? content = default(T);
        try
        {
            responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                content = JsonSerializer.Deserialize<T>(responseContent);
            else throw new Exception();

        }
        catch (Exception)
        {
            error = new()
            {
                Comment = response.StatusCode.ToString()
            };
        }

        if (error is not null)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("ERRJSON: " + errorJson);
                error!.ValidationResponse = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
            }
        }

        return new Response<T?>()
        {
            Content = content,
            Error = error,
            StatusCode = response.StatusCode
        };
    }

    public static NewsRoute News = new();
    public static CategoriesRoute Categories = new();
    public static AuthRoute Auth = new();
}