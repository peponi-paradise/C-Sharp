using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

public class SkillResponse
{
    [JsonPropertyName("version")]
    public string? Version { get; set; } = "2.0";

    [JsonPropertyName("template")]
    public SkillTemplate? Template { get; set; }
}