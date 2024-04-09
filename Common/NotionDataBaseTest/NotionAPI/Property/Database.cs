using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

// https://developers.notion.com/reference/property-object 에 따라 작성
public class DatabaseProperty
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? name { get; set; }
}

public class DatabaseSelect : DatabaseProperty
{
    public SelectOptions? select { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? equals { get; set; }
}

public class DatabaseTitle : DatabaseProperty
{
    public RichText? title { get; set; }
}