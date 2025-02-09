using System.Text.Json.Serialization;

namespace RequestsLibrary.Requests;
public class UpdateProfileRequest(string firstName, string lastName, string? avatar, string? password)
{
    [JsonPropertyName("firstName")  ] public string  FirstName   { get; set; } = firstName;
    [JsonPropertyName("lastName")   ] public string  LastName    { get; set; } = lastName;
    [JsonPropertyName("avatar")     ] public string? Avatar      { get; set; } = avatar;
    [JsonPropertyName("password")   ] public string? Password    { get; set; } = password;
}