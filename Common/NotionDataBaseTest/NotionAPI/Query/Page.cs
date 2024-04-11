using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

// https://developers.notion.com/reference/post-page 에 따라 작성
public class QueryPageItem
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Parent? parent { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, PageProperty>? properties { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object>? children { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? icon { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? cover { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? archived { get; set; }
}