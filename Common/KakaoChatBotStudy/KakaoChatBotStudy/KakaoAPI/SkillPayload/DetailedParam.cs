using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#detailparams 에 따라 작성
public class DetailParam
{
    [JsonPropertyName("groupName")]
    public string? GroupName { get; set; }

    [JsonPropertyName("origin")]
    public string? Origin { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}