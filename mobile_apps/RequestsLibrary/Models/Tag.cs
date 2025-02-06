using Newtonsoft.Json;

namespace RequestsLibrary.Models;
public class Tag
{
    [JsonProperty("id")] public long Id { get; set; }

    [JsonProperty("name")] public string? Name { get; set; }
    [JsonProperty("description")] public string? Description { get; set; }
}