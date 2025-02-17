﻿using Newtonsoft.Json;
using RequestsLibrary.Responses;
using RequestsLibrary.Routes;
using System.Diagnostics;

namespace RequestsLibrary;

public static class Fetcher
{
    public static Config Config = new(Config.Protocol.https, "news.zmln.ru");

    private static string? _token;

    private static readonly HttpClient _httpClient = new()
    {
        Timeout = TimeSpan.FromSeconds(3)
    };

    public static void SetToken(string? token)
    {
        _token = token;
    }

    public static Action ErrorConnectedAction = () => Debug.WriteLine("Ошибка подключения");

    public static async Task<Response<T?>> Fetch<T>(
        HttpMethod method,
        string url,
        RequestParams? rp = null
    )
    {
        // Инициализируем метод запроса
        var request = new HttpRequestMessage(method, url);

        Debug.WriteLine("\nГотовлюсь к запросу ...");

        if (rp is null)
            rp = new RequestParams();

        if (_token is not null)
            rp.AddHeader("Authorization", $"Bearer {_token}");

        // Добавляем нагрузку в запрос
        rp?.AddToRequest(request);

        Debug.WriteLine($"Url: {request.RequestUri}");
        Debug.WriteLine($"Token: {_token ?? "Нету"}");
        Debug.WriteLine($"HttpRequestMessage: {JsonConvert.SerializeObject(request)}");

        // Выполняем запрос
        HttpResponseMessage response;

        try
        {
            response = await _httpClient.SendAsync(request);
        }
        catch (Exception ex)
        {
            if (ex is TaskCanceledException tce)
                ErrorConnectedAction();

            return new Response<T?>()
            {
                Content = default,
                Error = new()
                {
                    Comment = "Ошибка запроса",
                },
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }

        // Читаем ответ и отдаём ответ
        string? responseContent = null;
        Error? error = null;
        T? content = default(T);
        try
        {
            responseContent = await response.Content.ReadAsStringAsync();

            Debug.WriteLine($"ResponseContent: {responseContent}");

            if (response.IsSuccessStatusCode)
                content = JsonConvert.DeserializeObject<T>(responseContent);
            else throw new Exception();

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ошибка чтения тела запроса: {ex.Message}");
            error = new()
            {
                Comment = response.StatusCode.ToString()
            };
        }

        if (error is not null)
        {
            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    var errorJson = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("ERRJSON: " + errorJson);
                    error!.ValidationResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorJson);
                } catch
                {
                    error.Comment = $"Не удалось обработать ошибку: {response.StatusCode}";
                }
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
    public static FavouriteRoute Favourite = new();
}