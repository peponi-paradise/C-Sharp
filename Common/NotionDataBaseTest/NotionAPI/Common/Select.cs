using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

// https://developers.notion.com/reference/property-object#select 에 따라 작성
public class Select
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? id { get; set; }

    public string? name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? color { get; set; }
}

public class SelectOptions
{
    public List<Select>? options { get; set; }
}