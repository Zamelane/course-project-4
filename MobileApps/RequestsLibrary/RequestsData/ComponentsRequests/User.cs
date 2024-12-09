using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RequestsLibrary.RequestsData.ComponentsRequests
{
    public class User
    {
        [JsonPropertyName("id")       ] public int Id            { get; set; }
        [JsonPropertyName("login")    ] public string? Login     { get; set; }
        [JsonPropertyName("email")    ] public string? Email     { get; set; }
        [JsonPropertyName("birthDay") ] public DateTime BirthDay { get; set; }
        [JsonPropertyName("firstName")] public string? FirstName { get; set; }
        [JsonPropertyName("lastName") ] public string? LastName  { get; set; }
        [JsonPropertyName("role")     ] public string? Role      { get; set; }
        [JsonPropertyName("avatar")   ] public string? Avatar    { get; set; }
    }
}
