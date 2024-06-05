using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#skillresponse에 따라 작성
public class SkillResponse
{
    [JsonPropertyName("version")]
    public string? Version { get; init; } = "2.0";

    [JsonPropertyName("template")]
    public SkillTemplate? Template { get; set; }
}