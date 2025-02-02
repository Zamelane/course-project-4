using Newtonsoft.Json;
using RequestsLibrary.Models;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Responses;
public class FavouriteResponse
{
    [JsonProperty("favourites")] public ObservableCollection<Favourite> Favourites { get; set; } = [];
    [JsonProperty("total")] public int Total { get; set; } = 0;
}
