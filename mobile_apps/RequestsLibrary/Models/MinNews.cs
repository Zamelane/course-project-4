using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Models;
public class MinNews
{
    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("title")] public string? Title { get; set; }

    [JsonProperty("content")] public string? Content { get; set; }

    [JsonProperty("city")] public City? City { get; set; }

    [JsonProperty("tags")] public ObservableCollection<Tag> Tags { get; set; }
        = new ObservableCollection<Tag>();

    [JsonProperty("reactions")] public ObservableCollection<Reaction> Reactions { get; set; } 
        = new ObservableCollection<Reaction>();

    [JsonProperty("create_date")] public DateTime CreateDate { get; set; }

    [JsonProperty("update_date")] public DateTime? UpdateDate { get; set; }
}