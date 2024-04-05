using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

public class DatabaseQuery
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DatabaseSelect? 선택 { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DatabaseTitle? 이름 { get; set; }
}

// https://developers.notion.com/reference/post-database-query 에 따라 작성
public class DatabaseFilterEntry
{
    public DatabaseFilter? filter { get; set; }
}

public class DatabaseFilter
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<DatabaseFilter>? and { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<DatabaseFilter>? or { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? property { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DatabaseSelect? select { get; set; }
}