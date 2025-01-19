using System.Text.Json.Serialization;

namespace RequestsLibrary.Models;
public class City
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("name")] public string? Name { get; set; }
}