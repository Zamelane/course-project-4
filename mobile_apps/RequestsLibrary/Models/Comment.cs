using Newtonsoft.Json;

namespace RequestsLibrary.Models;
public class Comment
{
    [JsonProperty("id")         ] public            int         Id          { get; set; }

    [JsonProperty("user")       ] public required   User        User        { get; set; }

    [JsonProperty("content")    ] public required   string      Content     { get; set; }

    [JsonProperty("created_at") ] public            DateTime    CreatedAt   { get; set; }
    [JsonProperty("updated_at") ] public            DateTime    UpdatedAt   { get; set; }
}
