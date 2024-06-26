﻿using System.Net.Http.Json;
using System.Text.Json;
using NotionAPI;
using NotionAPI.Objects;

namespace DatabaseQuery
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string baseUri = $"https://api.notion.com/v1/databases";
            string databaseKey = "데이터베이스 키";
            string APIKey = "API 키";

            Console.WriteLine(DefaultQuery(baseUri, databaseKey, APIKey));
            Console.WriteLine(PaginatedQuery(baseUri, databaseKey, APIKey));
            Console.WriteLine(ContinuedPaginatedQuery(baseUri, databaseKey, APIKey));
            Console.WriteLine(FilteredQuery(baseUri, databaseKey, APIKey));
        }

        private static bool DefaultQuery(string baseUri, string databaseKey, string APIKey)
        {
            HttpClient client = new();

            // https://developers.notion.com/reference/post-database-query 문서 내용에 따라 Post로 request 작성
            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}/{databaseKey}/query");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");

            var response = client.Send(request);

            // StatusCode를 포함한 Header 출력
            Console.WriteLine(response);
            // JSON 형식의 Body 출력
            var content = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
            Console.WriteLine(content);

            // Parsing
            var parsed = JsonSerializer.Deserialize<NotionResponse>(content);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        static bool PaginatedQuery(string baseUri, string databaseKey, string APIKey, int pageSize = 100)
        {
            HttpClient client = new();

            // https://developers.notion.com/reference/post-database-query 문서 내용에 따라 Post로 request 작성
            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}/{databaseKey}/query");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");
            // https://developers.notion.com/reference/intro#pagination 에 따라 Query 옵션 추가
            request.Content = JsonContent.Create(new PaginatedRequest { page_size = pageSize });

            var response = client.Send(request);

            // StatusCode를 포함한 Header 출력
            Console.WriteLine(response);
            // JSON 형식의 Body 출력
            Console.WriteLine(new StreamReader(response.Content.ReadAsStream()).ReadToEnd());

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private static bool ContinuedPaginatedQuery(string baseUri, string databaseKey, string APIKey, int pageSize = 100, string? startCursor = null)
        {
            bool hasMore = false;
            HttpClient client = new();

            do
            {
                // https://developers.notion.com/reference/post-database-query 문서 내용에 따라 Post로 request 작성
                var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}/{databaseKey}/query");
                request.Headers.Add("Authorization", $"Bearer {APIKey}");
                request.Headers.Add("Notion-Version", "2022-06-28");
                // https://developers.notion.com/reference/intro#pagination 에 따라 Query 옵션 추가
                request.Content = JsonContent.Create(new PaginatedRequest { page_size = pageSize, start_cursor = startCursor });

                var response = client.Send(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK) return false;

                // StatusCode를 포함한 Header 출력
                Console.WriteLine(response);
                // JSON 형식의 Body 출력
                string content = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
                Console.WriteLine(content);

                // 추가 데이터가 더 있는지, 다음 시작 위치는 어디인지 설정
                var parsed = JsonSerializer.Deserialize<NotionResponse>(content);
                if (parsed is not null)
                {
                    hasMore = parsed.has_more;
                    startCursor = parsed.next_cursor;
                }
            }
            while (hasMore);

            return true;
        }

        private static bool FilteredQuery(string baseUri, string databaseKey, string APIKey)
        {
            HttpClient client = new();

            // https://developers.notion.com/reference/post-database-query 문서 내용에 따라 Post로 request 작성
            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}/{databaseKey}/query");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");
            // https://developers.notion.com/reference/post-database-query 에 따라 Query 옵션 추가
            request.Content = JsonContent.Create(new DatabaseFilterEntry() { filter = new DatabaseFilter() { property = "선택", select = new() { equals = "1" } } });

            var response = client.Send(request);

            // StatusCode를 포함한 Header 출력
            Console.WriteLine(response);
            // JSON 형식의 Body 출력
            Console.WriteLine(new StreamReader(response.Content.ReadAsStream()).ReadToEnd());

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}