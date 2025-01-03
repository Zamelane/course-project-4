using RequestsLibrary.Responses.ComponentsResponses;
using System.Text.Json.Serialization;

namespace RequestsLibrary.Responses.Api.Category;
public class CategoryResponse : ErrorResponse
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("accent_color")] public string? AccentColor { get; set; }
    [JsonPropertyName("backgroud_color")] public DateTime BackgroundColor { get; set; }
    [JsonPropertyName("image")] public CategoryImageModel? Image { get; set; }
}