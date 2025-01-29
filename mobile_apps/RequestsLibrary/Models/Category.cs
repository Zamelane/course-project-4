using Newtonsoft.Json;

namespace RequestsLibrary.Models;
public class Category
{
    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("name")] public string? Name { get; set; }

    [JsonProperty("background_color")] public string? BackgroundColor { get; set; }

    [JsonProperty("image")] public Image? Image { get; set; }
}
