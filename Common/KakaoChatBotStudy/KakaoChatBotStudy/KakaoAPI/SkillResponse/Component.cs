using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

[JsonPolymorphic]
[JsonDerivedType(typeof(SimpleText))]
[JsonDerivedType(typeof(TextCard))]
public class Component
{
}

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#simpletext 에 따라 작성
public class SimpleText : Component
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#textcard 에 따라 작성
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

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#button 에 따라 작성
[JsonPolymorphic(TypeDiscriminatorPropertyName = "action")]
[JsonDerivedType(typeof(ButtonWithWebLink), typeDiscriminator: "webLink")]
public class Button
{
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("extra")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public dynamic? Extra { get; set; }
}

public class ButtonWithWebLink : Button
{
    [JsonPropertyName("webLinkUrl")]
    public string? WebLinkUrl { get; set; }
}