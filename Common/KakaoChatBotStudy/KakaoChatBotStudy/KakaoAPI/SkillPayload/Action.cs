using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

public class Action
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("params")]
    public Dictionary<string, string>? Params { get; set; }

    [JsonPropertyName("detailParams")]
    public Dictionary<string, DetailParam>? DetailParams { get; set; }

    [JsonPropertyName("clientExtra")]
    public object? ClientExtra { get; set; }
}