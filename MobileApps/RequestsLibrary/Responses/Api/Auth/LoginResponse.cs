using RequestsLibrary.RequestsData.ComponentsRequests;
using System.Text.Json.Serialization;

namespace RequestsLibrary.Responses.Api.Auth
{
    public class LoginResponse : ErrorResponse
    {
        [JsonPropertyName("success")] public bool    Success { get; set; }
        [JsonPropertyName("token")  ] public string? Token   { get; set; }
        [JsonPropertyName("user")   ] public User?   User    { get; set; }
    }
}
