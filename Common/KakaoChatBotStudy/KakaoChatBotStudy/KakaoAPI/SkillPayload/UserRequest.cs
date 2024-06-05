using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#userrequest 에 따라 작성
public class UserRequest
{
    [JsonPropertyName("block")]
    public Block? Block { get; set; }

    // 사용자 정보
    [JsonPropertyName("user")]
    public User? User { get; set; }

    [JsonPropertyName("utterance")]
    public string? Utterance { get; set; }

    [JsonPropertyName("lang")]
    public string? Language { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }
}

public class Block
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

// https://kakaobusiness.gitbook.io/main/tool/chatbot/skill_guide/answer_json_format#user 에 따라 작성
public class User
{
    // 봇에 대한 사용자 식별 키
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    // 추가 유저 정보 (채널 유저 키, 채널 추가 여부 등)
    [JsonPropertyName("properties")]
    public object? Properties { get; set; }
}