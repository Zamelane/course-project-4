using System.Text.Json.Serialization;

namespace RequestsLibrary.Requests;
public class AuthRequest(string login, string password)
{
    [JsonPropertyName("login")] public string Login { get; set; } = login;
    [JsonPropertyName("password")] public string Password { get; set; } = password;
}