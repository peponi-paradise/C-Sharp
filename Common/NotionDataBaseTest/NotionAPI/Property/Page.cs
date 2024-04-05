using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

// https://developers.notion.com/reference/page-property-values 에 따라 작성
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(PageSelect), typeDiscriminator: "select")]
[JsonDerivedType(typeof(PageTitle), typeDiscriminator: "title")]
public class PageProperty
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? id { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? type { get; set; }
}

public sealed class PageSelect : PageProperty
{
    public Select? select { get; set; }
}

public sealed class PageTitle : PageProperty
{
    public List<RichText>? title { get; set; }
}