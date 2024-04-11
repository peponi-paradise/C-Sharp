using System.Text.Json;
using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

// https://developers.notion.com/reference/page-property-values 에 따라 작성
[JsonConverter(typeof(PagePropertyConverter))]
public class PageProperty
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? id { get; set; }
}

public sealed class PageSelect : PageProperty
{
    public Select? select { get; set; }
}

public sealed class PageTitle : PageProperty
{
    public List<RichText>? title { get; set; }
}

public class PagePropertyConverter : JsonConverter<PageProperty>
{
    public override PageProperty? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDoc = JsonDocument.ParseValue(ref reader);
        if (jsonDoc.RootElement.TryGetProperty("type", out var typeName))
        {
            return typeName.GetString() switch
            {
                "select" => JsonSerializer.Deserialize<PageSelect>(jsonDoc),
                "title" => JsonSerializer.Deserialize<PageTitle>(jsonDoc),
                _ => null
            };
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, PageProperty value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case PageSelect select:
                JsonSerializer.Serialize(writer, select);
                break;

            case PageTitle title:
                JsonSerializer.Serialize(writer, title);
                break;
        }
    }
}