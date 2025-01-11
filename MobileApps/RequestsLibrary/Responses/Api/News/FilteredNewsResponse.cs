using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using RequestsLibrary.Responses.ComponentsResponses;

namespace RequestsLibrary.Responses.Api.News;

public class FilteredNewsResponse : ErrorResponse
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("content")] public string? Content { get; set; }
    [JsonPropertyName("city")] public CityModel? City { get; set; }
    [JsonPropertyName("tags")] public ObservableCollection<string>? Tags { get; set; }
    [JsonPropertyName("reactions")] public dynamic? Reactions { get; set; }
    [JsonPropertyName("create_date")] public DateTime? CreateDate { get; set; }
    [JsonPropertyName("update_date")] public DateTime? UpdateDate { get; set; }
}