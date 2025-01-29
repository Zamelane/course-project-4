using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Models;
public class FullNews : MinNews
{
    [JsonProperty("author")] public User? Author { get; set; }
    [JsonProperty("images")] public ObservableCollection<dynamic> Images { get; set; } = [];
    [JsonProperty("isBookmarked")] public bool IsBookmarked { get; set; } = false;
    [JsonProperty("comments")] public int Comments { get; set; } = 0;
    [JsonProperty("views")] public int Views { get; set; } = 0;
}
