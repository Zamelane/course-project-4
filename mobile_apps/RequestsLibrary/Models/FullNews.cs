using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace RequestsLibrary.Models;
public class FullNews : MinNews
{
    [JsonPropertyName("author")] public User? Author { get; set; }
    [JsonPropertyName("images")] public ObservableCollection<dynamic> Images { get; set; } = [];
    [JsonPropertyName("isBookmarked")] public bool IsBookmarked { get; set; } = false;
    [JsonPropertyName("comments")] public int Comments { get; set; } = 0;
    [JsonPropertyName("views")] public int Views { get; set; } = 0;
}
