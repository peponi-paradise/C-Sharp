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
            string APIKey = "API 키";

            Console.WriteLine(Create(baseUri, databaseKey, APIKey));
        }

        private static bool Create(string baseUri, string databaseKey, string APIKey)
        {
            HttpClient client = new();

            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");
            var addItem = new CreatePageItem();
            addItem.parent = new DatabaseParent() { database_id = databaseKey };
            var properties = new Dictionary<string, PageProperty>
            {
                { "이름", new PageTitle() { title = [new RichTextWithText() { text = new() { content = "5" } }] } },
                { "선택", new PageSelect() { select = new() { name = "2" } } }
            };
            addItem.properties = properties;
            request.Content = JsonContent.Create(addItem, null, new JsonSerializerOptions() { IncludeFields = true });

            var response = client.Send(request);

            // StatusCode를 포함한 HTTP response 출력
            Console.WriteLine(response);
            // JSON 형식의 response data 출력
            Console.WriteLine(new StreamReader(response.Content.ReadAsStream()).ReadToEnd());

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}