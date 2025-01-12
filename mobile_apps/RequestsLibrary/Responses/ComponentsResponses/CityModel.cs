using System.Text.Json.Serialization;

namespace RequestsLibrary.Responses.ComponentsResponses;

public class CityModel
{
    [JsonPropertyName("id")] private int Id { get; set; }
    [JsonPropertyName("name")] private string? Name { get; set; }
}