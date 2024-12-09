using RequestsLibrary.Types;
using System.Text.Json;

namespace RequestsLibrary
{
    public static class Fetcher
    {
        public static Uri API_URL = new("http://127.0.0.1:8000/api/");
        private static string? _token = null;

        public static void SetToken(string? token) => _token = token;
        internal static string? GetToken() => _token;

        private readonly static HttpClient _httpClient = new() { BaseAddress = API_URL };

        public static async Task<(HttpResponseMessage, T?, E?)> SendCustomRequest<T, E>(
            HttpMethod method,
            string     path,
            Headers[]? headers = null,
            Content?   body    = null
        ) {
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
                return (response, default(T), default(E));
            }
        }
    }
}
