﻿using NotionAPI.Objects;
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
    public List<PageInformation>? results { get; set; }

    // results의 데이터 형식
    public string? type { get; set; }
}

// https://developers.notion.com/reference/intro#parameters-for-paginated-requests 에 따라 작성
public class PaginatedRequest
{
    // 기본값 및 최대값은 100
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int page_size { get; set; }

    // 시작점이 정의되지 않으면 처음부터 조회
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? start_cursor { get; set; }
}