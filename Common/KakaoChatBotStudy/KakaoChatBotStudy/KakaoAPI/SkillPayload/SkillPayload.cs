using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

public class SkillPayload
{
    [JsonPropertyName("bot")]
    public Dictionary<string, string>? Bot { get; set; }

    [JsonPropertyName("intent")]
    public Intent? Intent { get; set; }

    [JsonPropertyName("action")]
    public Action? Action { get; set; }

    [JsonPropertyName("userRequest")]
    public UserRequest? UserRequest { get; set; }

    [JsonPropertyName("contexts")]
    public List<object>? Contexts { get; set; }
}