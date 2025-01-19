using System.Text.Json.Serialization;

namespace RequestsLibrary.Models;
public class User
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("firstName")] public string? FirstName { get; set; }
    [JsonPropertyName("lastName")] public string? LastName { get; set; }
    [JsonPropertyName("login")] public string? Login { get; set; }
    [JsonPropertyName("birthDay")] public DateTime BirthDay { get; set; }
    [JsonPropertyName("email")] public string? Email { get; set; }
    [JsonPropertyName("role")] public string? Role { get; set; }
    [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updated_at")] public DateTime? UpdatedAt { get; set; }
}