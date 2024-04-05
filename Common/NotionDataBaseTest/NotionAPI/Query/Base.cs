using System.Text.Json.Serialization;

namespace NotionAPI;

// https://developers.notion.com/reference/intro#responses 에 따라 작성
public class NotionResponse
{
    // 추가로 더 읽을 수 있는 데이터가 있는지
    public bool has_more { get; set; }

    // has_more가 true인 경우 다음 시작점
    public string? next_cursor { get; set; }

    public string? @object { get; set; }

    // 데이터가 들어감
    public List<object>? results { get; set; }

    // results의 데이터 형식
    public string? type { get; set; }

    // 아래는 웹페이지 정보에는 없지만, 통신 받을 때 같이 들어옴
    // 필요할 때 사용
    // public object? page_or_database { get; set; }
    // public string? request_id { get; set; }
}

public class PaginatedRequest
{
    // 기본값 및 최대값은 100
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int page_size { get; set; }

    // 시작점이 정의되지 않으면 처음부터 조회
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? start_cursor { get; set; }
}