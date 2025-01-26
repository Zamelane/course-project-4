using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace RequestsLibrary.Models;
public class FullNews : MinNews
{
    [JsonPropertyName("author")] public User? Author { get; set; }
    [JsonPropertyName("images")] public ObservableCollection<dynamic> Images { get; set; } = [];
}
