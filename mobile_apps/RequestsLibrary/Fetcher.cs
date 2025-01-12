using System.Text.Json;
using RequestsLibrary.Types;

namespace RequestsLibrary;

public static class Fetcher
{
    public static Uri URL = new("http://192.168.1.100:8000/");
    private static string? _token;

    private static readonly HttpClient _httpClient = new() { BaseAddress = API_URL };
    public static Uri API_URL => new($"{URL}api/");

    public static void SetToken(string? token)
    {
        _token = token;
    }

    internal static string? GetToken()
    {
        return _token;
    }

    public static async Task<(HttpResponseMessage, T?, E?)> SendCustomRequest<T, E>(
        HttpMethod method,
        string path,
        Headers[]? headers = null,
        Content? body = null
    )
    {
        // Инициализируем метод запроса
        var request = new HttpRequestMessage(method, path);

        // Добавляем заголовки в запрос
        if (headers != null)
            foreach (var header in headers)
                header.AddToRequest(request);

        // Подготавливаем тело к отправке
        if (body != null)
            body.AddToRequest(request);

        // Выполняем запрос
        var response = await _httpClient.SendAsync(request);

        // Читаем ответ и отдаём ответ
        string? responseContent = null;
        try
        {
            responseContent = await response.Content.ReadAsStringAsync();
            var exception = default(E);

            if (!response.IsSuccessStatusCode)
                exception = JsonSerializer.Deserialize<E>(responseContent);
            return (response, JsonSerializer.Deserialize<T>(responseContent), exception);
        }
        catch (Exception) when (!response.IsSuccessStatusCode)
        {
            return (response, default, default);
        }
    }
}