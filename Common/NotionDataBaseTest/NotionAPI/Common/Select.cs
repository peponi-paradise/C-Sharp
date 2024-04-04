using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

public class Select
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? id { get; set; }

    public string? name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? color { get; set; }
}