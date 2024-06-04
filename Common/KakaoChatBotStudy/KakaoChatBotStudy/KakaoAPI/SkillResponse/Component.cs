using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

[JsonPolymorphic]
[JsonDerivedType(typeof(SimpleText))]
[JsonDerivedType(typeof(TextCard))]
public class Component
{
}

public class SimpleText : Component
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}

public class TextCard : Component
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("buttons")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Button>? Buttons { get; set; }
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "action")]
[JsonDerivedType(typeof(ButtonWithWebLink), typeDiscriminator: "webLink")]
public class Button
{
    [JsonPropertyName("label")]
    public string? Label { get; set; }
}

public class ButtonWithWebLink : Button
{
    [JsonPropertyName("webLinkUrl")]
    public string? WebLinkUrl { get; set; }
}