using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#skillpayload 에 따라 작성
public class SkillPayload
{
    // 봇의 정보
    [JsonPropertyName("bot")]
    public Bot? Bot { get; set; }

    // 블록 정보
    [JsonPropertyName("intent")]
    public Intent? Intent { get; set; }

    // 봇을 통해 들어온 데이터 및 스킬 정보
    [JsonPropertyName("action")]
    public Action? Action { get; set; }

    // 사용자 정보
    [JsonPropertyName("userRequest")]
    public UserRequest? UserRequest { get; set; }

    [JsonPropertyName("contexts")]
    public List<object>? Contexts { get; set; }
}

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#bot 에 따라 작성
public class Bot
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}