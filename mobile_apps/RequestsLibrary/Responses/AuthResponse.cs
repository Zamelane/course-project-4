using RequestsLibrary.Models;
using System.Text.Json.Serialization;

namespace RequestsLibrary.Requests;
public class AuthResponse
{
    [JsonPropertyName("success")] public bool Success { get; set; }

    [JsonPropertyName("token")] public string? Token { get; set; }

    [JsonPropertyName("user")] public User? User { get; set; }
}
