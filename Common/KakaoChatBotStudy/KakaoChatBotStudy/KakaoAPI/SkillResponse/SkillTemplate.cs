using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#skilltemplate 에 따라 작성
public class SkillTemplate
{
    // 1개 이상 3개 이하 필수 포함
    [JsonPropertyName("outputs")]
    public List<Dictionary<string, Component>>? Outputs { get; set; }

    [JsonPropertyName("quickReplies")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<QuickReply>? QuickReplies { get; set; }
}