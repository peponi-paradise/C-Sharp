using System.Net.Http.Json;
using System.Text.Json;
using NotionAPI;

namespace DatabaseQuery
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string baseUri = $"https://api.notion.com/v1/databases";
            string databaseKey = "데이터베이스 키";
            string APIKey = "API 키";
            int dataSize = 50;

            Console.WriteLine(DefaultQuery(baseUri, databaseKey, APIKey));
            Console.WriteLine(PaginatedQuery(baseUri, databaseKey, APIKey, dataSize));
            Console.WriteLine(ContinuedPaginatedQuery(baseUri, databaseKey, APIKey, dataSize));
        }

        static bool DefaultQuery(string baseUri, string databaseKey, string APIKey)
        {
            HttpClient client = new();

            // https://developers.notion.com/reference/post-database-query 문서 내용에 따라 Post로 request 작성
            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}/{databaseKey}/query");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");

            var response = client.Send(request);

            // StatusCode를 포함한 HTTP response 출력
            Console.WriteLine(response);
            // JSON 형식의 response data 출력
            Console.WriteLine(new StreamReader(response.Content.ReadAsStream()).ReadToEnd());

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        static bool PaginatedQuery(string baseUri, string databaseKey, string APIKey, int dataSize)
        {
            HttpClient client = new();

            // https://developers.notion.com/reference/post-database-query 문서 내용에 따라 Post로 request 작성
            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}/{databaseKey}/query");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");
            // Query 옵션 추가
            request.Content = JsonContent.Create(new PaginatedRequest { page_size = dataSize });

            var response = client.Send(request);

            // StatusCode를 포함한 HTTP response 출력
            Console.WriteLine(response);
            // JSON 형식의 response data 출력
            Console.WriteLine(new StreamReader(response.Content.ReadAsStream()).ReadToEnd());

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        static bool ContinuedPaginatedQuery(string baseUri, string databaseKey, string APIKey, int dataSize, string? startCursor = null)
        {
            bool hasMore = false;

            HttpClient client = new();

            do
            {
                // https://developers.notion.com/reference/post-database-query 문서 내용에 따라 Post로 request 작성
                var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}/{databaseKey}/query");
                request.Headers.Add("Authorization", $"Bearer {APIKey}");
                request.Headers.Add("Notion-Version", "2022-06-28");
                // Query 옵션 추가
                request.Content = JsonContent.Create(new PaginatedRequest { page_size = dataSize, start_cursor = startCursor });

                var response = client.Send(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK) return false;

                // StatusCode를 포함한 HTTP response 출력
                Console.WriteLine(response);
                // JSON 형식의 response data 출력
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
    }
}