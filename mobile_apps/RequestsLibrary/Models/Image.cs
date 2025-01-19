using System.Text.Json.Serialization;

namespace RequestsLibrary.Models;
public class Image
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("path")] public string? Path { get; set; }
}