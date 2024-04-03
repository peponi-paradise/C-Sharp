using System.Text.Json.Serialization;

namespace NotionAPI.Objects;

// https://developers.notion.com/reference/parent-object 에 따라 작성
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(DatabaseParent), typeDiscriminator: "database_id")]
[JsonDerivedType(typeof(PageParent), typeDiscriminator: "page_id")]
[JsonDerivedType(typeof(WorkspaceParent), typeDiscriminator: "workspace")]
[JsonDerivedType(typeof(BlockParent), typeDiscriminator: "block_id")]
public class Parent
{
}

public sealed class DatabaseParent : Parent
{
    public string? database_id { get; set; }
}

public sealed class PageParent : Parent
{
    public string? page_id { get; set; }
}

public sealed class WorkspaceParent : Parent
{
    public bool workspace { get; set; } = true;
}

public sealed class BlockParent : Parent
{
    public string? block_id { get; set; }
}