using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#action 에 따라 작성
public class Action
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    // 블록에 설정한 파라미터, 값이 들어감
    [JsonPropertyName("params")]
    public Dictionary<string, string>? Params { get; set; }

    // 블록에 설정한 파라미터, 값에 대한 상세 정보
    // (발화 원본 데이터 포함)
    [JsonPropertyName("detailParams")]
    public Dictionary<string, DetailParam>? DetailParams { get; set; }

    // 바로가기 응답 같은 경우 여기로 들어온다 함
    // 필요한 경우 Dictionary<string, object>로 구현 가능
    [JsonPropertyName("clientExtra")]
    public object? ClientExtra { get; set; }
}