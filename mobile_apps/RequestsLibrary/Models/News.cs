using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace RequestsLibrary.Models;
public class News
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("title")] public string? Title { get; set; }

    [JsonPropertyName("content")] public string? Content { get; set; }

    [JsonPropertyName("city")] public City? City { get; set; }

    [JsonPropertyName("tags")] public ObservableCollection<Tag> Tags { get; set; }
        = new ObservableCollection<Tag>();

    [JsonPropertyName("reactions")] public ObservableCollection<Reaction> Reactions { get; set; } 
        = new ObservableCollection<Reaction>();

    [JsonPropertyName("create_date")] public DateTime CreateDate { get; set; }

    [JsonPropertyName("update_date")] public DateTime? UpdateDate { get; set; }
}