using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#field_details-9 에 따라 작성
public class QuickReply
{
    // 바로가기 응답 버튼의 텍스트
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    // Message or block
    [JsonPropertyName("action")]
    public string? Action { get; init; }

    // 응답 선택 시 유저에게 출력할 텍스트
    [JsonPropertyName("messageText")]
    public string? MessageText { get; set; }

    // Required when Action is block
    [JsonPropertyName("blockId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BlockId { get; set; }

    [JsonPropertyName("extra")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public dynamic? Extra { get; set; }
}