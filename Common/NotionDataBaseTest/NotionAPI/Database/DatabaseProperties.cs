using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

public class DatabaseSelect
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? type { get; set; }

    public Select? select { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? equals { get; set; }
}

public class DatabaseTitle
{
    public string? id { get; set; }
    public string? name { get; set; }
    public string? type { get; set; }
    public object? title { get; set; }
}

public class Select
{
    public Option[]? options { get; set; }
}

public class Option
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? id { get; set; }

    public string? name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? color { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? description { get; set; }
}