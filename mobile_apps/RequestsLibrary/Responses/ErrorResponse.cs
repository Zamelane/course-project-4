using Newtonsoft.Json;

namespace RequestsLibrary.Responses;
public class ErrorResponse
{
    [JsonProperty("code")] public int? Code { get; set; }
    [JsonProperty("message")] public string? Message { get; set; }
    [JsonProperty("errors")] public Dictionary<string, List<string>>? Errors { get; set; }
}
