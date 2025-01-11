using System.Text.Json.Serialization;

namespace RequestsLibrary.Responses.ComponentsResponses;

public class CategoryImageModel
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("path")] public string? Path { get; set; }
}