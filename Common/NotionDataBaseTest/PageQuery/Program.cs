using NotionAPI.Objects;
using System.Net.Http.Json;
using System.Text.Json;

namespace PageQuery
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string baseUri = $"https://api.notion.com/v1/pages";
            string databaseKey = "데이터베이스 키";
            string pageKey = "Page 키";
            string APIKey = "API 키";

            Console.WriteLine(Create(baseUri, databaseKey, APIKey));
            Console.WriteLine(Retrieve(baseUri, pageKey, APIKey));
            Console.WriteLine(Update(baseUri, pageKey, APIKey));
            Console.WriteLine(Archieve(baseUri, pageKey, APIKey));
        }

        private static bool Create(string baseUri, string databaseKey, string APIKey)
        {
            HttpClient client = new();

            // https://developers.notion.com/reference/post-page 문서 내용에 따라 Post로 request 작성
            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");
            var queryItem = new QueryPageItem();
            queryItem.parent = new DatabaseParent() { database_id = databaseKey };
            var properties = new Dictionary<string, PageProperty>
            {
                { "이름", new PageTitle() { title = [new RichTextWithText() { text = new() { content = "5" } }] } },
                { "선택", new PageSelect() { select = new() { name = "2" } } }
            };
            queryItem.properties = properties;
            request.Content = JsonContent.Create(queryItem);

            var response = client.Send(request);

            // StatusCode를 포함한 Header 출력
            Console.WriteLine(response);
            // JSON 형식의 Body 출력
            Console.WriteLine(new StreamReader(response.Content.ReadAsStream()).ReadToEnd());

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private static bool Retrieve(string baseUri, string pageKey, string APIKey)
        {
            HttpClient client = new();

            // https://developers.notion.com/reference/retrieve-a-page 문서 내용에 따라 Get으로 request 작성
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/{pageKey}");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");

            var response = client.Send(request);

            // StatusCode를 포함한 Header 출력
            Console.WriteLine(response);
            // JSON 형식의 Body 출력
            var content = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
            Console.WriteLine(content);

            var parsed = JsonSerializer.Deserialize<PageInformation>(content);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private static bool Update(string baseUri, string pageKey, string APIKey)
        {
            HttpClient client = new();

            // https://developers.notion.com/reference/patch-page 문서 내용에 따라 Patch로 request 작성
            var request = new HttpRequestMessage(HttpMethod.Patch, $"{baseUri}/{pageKey}");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");
            var queryItem = new QueryPageItem();
            var properties = new Dictionary<string, PageProperty>()
            {
                { "선택" , new PageSelect() { select = new() { name = "1" } } }
            };
            queryItem.properties = properties;
            request.Content = JsonContent.Create(queryItem);

            var response = client.Send(request);

            // StatusCode를 포함한 Header 출력
            Console.WriteLine(response);
            // JSON 형식의 Body 출력
            var content = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
            Console.WriteLine(content);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        // Notion API는 페이지 삭제를 지원하지 않는다.
        private static bool Archieve(string baseUri, string pageKey, string APIKey)
        {
            HttpClient client = new();

            // https://developers.notion.com/reference/archive-a-page 문서 내용에 따라 Patch로 request 작성
            var request = new HttpRequestMessage(HttpMethod.Patch, $"{baseUri}/{pageKey}");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");
            var queryItem = new QueryPageItem();
            queryItem.archived = false;
            request.Content = JsonContent.Create(queryItem);

            var response = client.Send(request);

            // StatusCode를 포함한 Header 출력
            Console.WriteLine(response);
            // JSON 형식의 Body 출력
            var content = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
            Console.WriteLine(content);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}