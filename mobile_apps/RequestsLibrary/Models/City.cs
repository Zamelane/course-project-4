using Newtonsoft.Json;

namespace RequestsLibrary.Models;
public class City
{
    [JsonProperty("id")] public long Id { get; set; }

    [JsonProperty("name")] public string? Name { get; set; }
}