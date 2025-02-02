using Newtonsoft.Json;

namespace RequestsLibrary.Models;
public class Image
{
    [JsonProperty("id")] public int Id { get; set; }
    [JsonProperty("hash")] public string? Hash { get; set; } = null;
    [JsonProperty("path")] public string? Path { get; set; } = null;
    [JsonProperty("url")] public string? Url { get; set; } = null;

    public string TotalPath
    {
        get
        {
            return $"{Fetcher.Config.GetServerUrl()}/{Url}";
        }
    }
}