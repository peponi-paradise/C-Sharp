using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

// https://developers.notion.com/reference/page-property-values 에 따라 작성
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