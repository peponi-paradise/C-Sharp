using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

// https://developers.notion.com/reference/rich-text 에 따라 작성
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(RichTextWithText), typeDiscriminator: "text")]
[JsonDerivedType(typeof(RichTextWithMention), typeDiscriminator: "mention")]
[JsonDerivedType(typeof(RichTextWithEquation), typeDiscriminator: "equation")]
public class RichText
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Annotations? annotations { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? plain_text { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? href { get; set; }
}

public sealed class RichTextWithText : RichText
{
    public Text? text { get; set; }
}

public sealed class RichTextWithMention : RichText
{
    public object? mention { get; set; }
}

public sealed class RichTextWithEquation : RichText
{
    public object? equation { get; set; }
}

public class Annotations
{
    public bool bold { get; set; }
    public bool italic { get; set; }
    public bool strikethrough { get; set; }
    public bool underline { get; set; }
    public bool code { get; set; }
    public string? color { get; set; } = "default";
}

public class Text
{
    public string? content { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Link? link { get; set; }
}

public class Link
{
    public string? url { get; set; }
}