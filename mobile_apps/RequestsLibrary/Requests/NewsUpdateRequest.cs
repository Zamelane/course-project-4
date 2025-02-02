using System.Text.Json.Serialization;

namespace RequestsLibrary.Requests;
public class NewsUpdateRequest(string title, string content, string? cover = null)
{
    [JsonPropertyName("title")]     public string Title     { get; set; } = title;
    [JsonPropertyName("content")]   public string Content   { get; set; } = content;
    [JsonPropertyName("cover")]     public string? Cover    { get; set; } = cover;
}