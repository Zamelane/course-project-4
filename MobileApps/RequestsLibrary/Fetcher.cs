using RequestsLibrary.Exceptions;
using RequestsLibrary.Responses;
using RequestsLibrary.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RequestsLibrary
{
    public static class Fetcher
    {
        public static Uri API_URL = new("http://localhost:8000/api/");

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

                if (!response.IsSuccessStatusCode)
                    return (response, default(T), JsonSerializer.Deserialize<E>(responseContent));
                return (response, JsonSerializer.Deserialize<T>(responseContent), default(E));
            }
            catch (Exception) when (!response.IsSuccessStatusCode)
            {
                return (response, default(T), default(E));
            }
        }
    }
}
