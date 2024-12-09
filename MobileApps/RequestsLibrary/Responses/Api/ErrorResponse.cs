using ObservableDictionary;
using System.Text.Json.Serialization;

namespace RequestsLibrary.Responses.Api
{
    public class ErrorResponse
    {
        [JsonPropertyName("code")] public int? Code { get; set; }
        [JsonPropertyName("message")] public string? Message { get; set; }
        [JsonPropertyName("errors")] public ObservableStringDictionary<List<string>>? Errors { get; set; }
    }
}
