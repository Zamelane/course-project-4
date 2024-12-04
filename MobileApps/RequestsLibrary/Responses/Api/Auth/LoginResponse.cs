using RequestsLibrary.RequestsData.ComponentsRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RequestsLibrary.Responses.Api.Auth
{
    public class LoginResponse
    {
        [JsonPropertyName("success")] public bool    Success { get; set; }
        [JsonPropertyName("token")  ] public string? Token   { get; set; }
        [JsonPropertyName("user")   ] public User?   User    { get; set; }
    }
}
