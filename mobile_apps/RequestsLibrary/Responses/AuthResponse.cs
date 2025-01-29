using Newtonsoft.Json;
using RequestsLibrary.Models;

namespace RequestsLibrary.Responses;
public class AuthResponse
{
    [JsonProperty("success")] public bool Success { get; set; }

    [JsonProperty("token")] public string? Token { get; set; }

    [JsonProperty("user")] public User? User { get; set; }
}
