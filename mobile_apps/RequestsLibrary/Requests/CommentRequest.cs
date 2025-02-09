using System.Text.Json.Serialization;

namespace RequestsLibrary.Requests;
public class CommentRequest(string content)
{
    [JsonPropertyName("content")] public string Content { get; set; } = content;
}