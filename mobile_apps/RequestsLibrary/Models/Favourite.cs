using System.Text.Json.Serialization;

namespace RequestsLibrary.Models;
public class Favourite
{
    [JsonPropertyName("news")] public MinNews News { get; set; }
    [JsonPropertyName("added_date")] public DateTime AddedDate { get; set; }
}
