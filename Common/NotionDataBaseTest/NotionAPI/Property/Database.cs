using System.Text.Json;
using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

// https://developers.notion.com/reference/property-object 에 따라 작성
[JsonConverter(typeof(DatabasePropertyConverter))]
public class DatabaseProperty
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? name { get; set; }
}

public class DatabaseSelect : DatabaseProperty
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SelectOptions? select { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? equals { get; set; }
}

public class DatabaseTitle : DatabaseProperty
{
    public RichText? title { get; set; }
}

public class DatabasePropertyConverter : JsonConverter<DatabaseProperty>
{
    public override DatabaseProperty? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDoc = JsonDocument.ParseValue(ref reader);
        if (jsonDoc.RootElement.TryGetProperty("type", out var typeName))
        {
            return typeName.GetString() switch
            {
                "select" => JsonSerializer.Deserialize<DatabaseSelect>(jsonDoc),
                "title" => JsonSerializer.Deserialize<DatabaseTitle>(jsonDoc),
                _ => null
            };
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, DatabaseProperty value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case DatabaseSelect select:
                JsonSerializer.Serialize(writer, select);
                break;

            case DatabaseTitle title:
                JsonSerializer.Serialize(writer, title);
                break;
        }
    }
}