using Newtonsoft.Json;
using System.Diagnostics;

namespace RequestsLibrary.Models;
public class User
{
    [JsonProperty("id")         ] public int        Id          { get; set; }
    [JsonProperty("firstName")  ] public string?    FirstName   { get; set; }
    [JsonProperty("lastName")   ] public string?    LastName    { get; set; }
    [JsonProperty("login")      ] public string?    Login       { get; set; }
    [JsonProperty("avatar")     ] public string?    PathAvatar  { get; set; }
    [JsonProperty("email")      ] public string?    Email       { get; set; }
    [JsonProperty("role")       ] public string?    Role        { get; set; }
    [JsonProperty("created_at") ] public DateTime?  CreatedAt   { get; set; }
    [JsonProperty("updated_at") ] public DateTime?  UpdatedAt   { get; set; }

    public Image? Avatar
    {
        get
        {
            if (PathAvatar is null) return null;
            Debug.WriteLine("Avatar: " + PathAvatar);
            return new Image()
            {
                Id = 0,
                Url = PathAvatar
            };
        }
    }
}