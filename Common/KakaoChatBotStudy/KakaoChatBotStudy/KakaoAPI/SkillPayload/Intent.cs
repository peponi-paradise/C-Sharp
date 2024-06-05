using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#intent 에 따라 작성
public class Intent
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    // 필요 없을듯
    [JsonPropertyName("extra")]
    public dynamic? Extra { get; set; }
}