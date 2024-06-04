using System.Text.Json.Serialization;

namespace KakaoChatBotStudy.KakaoAPI;

public class UserRequest
{
    [JsonPropertyName("block")]
    public Block? Block { get; set; }

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

public class User
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("properties")]
    public object? Properties { get; set; }
}