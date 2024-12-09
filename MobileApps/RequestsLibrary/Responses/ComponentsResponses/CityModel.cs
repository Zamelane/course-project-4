using System.Text.Json.Serialization;

namespace RequestsLibrary.Responses.ComponentsResponses;
public class CityModel
{
    [JsonPropertyName("id")] int Id { get; set; }
    [JsonPropertyName("name")] string? Name { get; set; }
}
