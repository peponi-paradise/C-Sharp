using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

public class DetailParam
{
    [JsonPropertyName("groupName")]
    public string? GroupName { get; set; }

    [JsonPropertyName("origin")]
    public string? Origin { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }
}