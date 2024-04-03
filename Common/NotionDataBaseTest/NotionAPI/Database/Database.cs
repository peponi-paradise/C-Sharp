namespace NotionAPI.Objects;

// https://developers.notion.com/reference/database 에 따라 작성
public class DatabaseInformation
{
    public string? @object { get; set; }
    public string? id { get; set; }
    public DateTime created_time { get; set; }
    public User? created_by { get; set; }
    public DateTime last_edited_time { get; set; }
    public User? last_edited_by { get; set; }
    public RichText[]? title { get; set; }
    public RichText[]? description { get; set; }
    public object? icon { get; set; }
    public object? cover { get; set; }
    public object? properties { get; set; }
    public Parent? parent { get; set; }
    public string? url { get; set; }
    public bool archived { get; set; }
    public bool is_inline { get; set; }
    public string? public_url { get; set; }

    // 아래는 웹페이지 정보에는 없지만, 통신 받을 때 같이 들어옴
    // 필요할 때 사용
    // public string? request_id { get; set; }
}