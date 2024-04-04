using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

// https://developers.notion.com/reference/property-object 에 따라 작성
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(DatabaseSelect), typeDiscriminator: "select")]
[JsonDerivedType(typeof(DatabaseTitle), typeDiscriminator: "title")]
public class DatabaseProperty
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? type { get; set; }
}

public class DatabaseSelect : DatabaseProperty
{
    public SelectOptions? select { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? equals { get; set; }
}

public class DatabaseTitle : DatabaseProperty
{
    public object? title { get; set; }
}

public class SelectOptions
{
    public Select[]? options { get; set; }
}