using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

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