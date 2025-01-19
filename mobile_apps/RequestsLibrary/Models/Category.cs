using System.Text.Json.Serialization;

namespace RequestsLibrary.Models;
public class Category
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("name")] public string? Name { get; set; }

    [JsonPropertyName("background_color")] public string? BackgroundColor { get; set; }

    [JsonPropertyName("image")] public Image? Image { get; set; }
}
