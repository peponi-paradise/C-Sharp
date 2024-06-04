using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

public class SkillTemplate
{
    [JsonPropertyName("outputs")]
    public List<Dictionary<string, Component>>? Outputs { get; set; }
}