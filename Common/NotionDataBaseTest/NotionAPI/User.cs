using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

// https://developers.notion.com/reference/user 에 따라 작성
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(People), typeDiscriminator: "person")]
[JsonDerivedType(typeof(Bots), typeDiscriminator: "bot")]
public class User
{
    public string? @object { get; set; }
    public string? id { get; set; }
    public string? name { get; set; }
    public string? avatar_url { get; set; }
}

public sealed class People : User
{
    public object? person { get; set; }
}

public sealed class Bots : User
{
    public object? bot { get; set; }
}