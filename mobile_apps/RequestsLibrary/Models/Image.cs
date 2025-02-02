using Newtonsoft.Json;

namespace RequestsLibrary.Models;
public class Image
{
    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("path")] public string? Path { get; set; }

    public string TotalPath
    {
        get
        {
            return $"{Fetcher.Config.GetServerUrl()}/{Path}";
        }
    }
}