using System.Text.Json.Serialization;

namespace RequestsLibrary.Requests;
public class FavouriteRequest(int id)
{
    [JsonPropertyName("news_id")] public int NewsId { get; set; } = id;
}