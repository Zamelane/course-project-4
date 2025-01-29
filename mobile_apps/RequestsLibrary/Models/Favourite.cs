using Newtonsoft.Json;

namespace RequestsLibrary.Models;
public class Favourite
{
    [JsonProperty("news")] public MinNews? News { get; set; }
    [JsonProperty("added_date")] public DateTime AddedDate { get; set; }
}
