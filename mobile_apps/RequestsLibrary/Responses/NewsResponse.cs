using Newtonsoft.Json;
using RequestsLibrary.Models;
using System.Collections.ObjectModel;

namespace RequestsLibrary.Responses;
public class NewsResponse
{
    [JsonProperty("news")] public ObservableCollection<MinNews> News { get; set; } = [];
    [JsonProperty("total")] public int Total { get; set; } = 0;
}
