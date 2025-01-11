using System.Text;
using System.Text.Json;

namespace RequestsLibrary.Types;

public class Content
{
    public enum ContentType
    {
        JSON,
        HttpContent
    }

    public dynamic? Value { get; set; }
    public ContentType? ValueType { get; set; }

    private dynamic ToJSON(dynamic content)
    {
        return JsonSerializer.Serialize(content);
    }

    public void AddToRequest(HttpRequestMessage request)
    {
        if (ValueType == null)
            if (Value is HttpContent)
                ValueType = ContentType.HttpContent;

        if (ValueType == ContentType.JSON)
            Value = new StringContent(ToJSON(Value), Encoding.UTF8, "application/json");
        else if (ValueType == null)
            throw new ArgumentException("Переданный тип данных не поддерживается или указан неверно");

        request.Content = Value;
    }
}