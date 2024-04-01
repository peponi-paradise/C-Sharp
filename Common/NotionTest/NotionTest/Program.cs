using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using JsonObject;
using System.Text.Json.Serialization;

namespace NotionTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string baseUri = $"https://api.notion.com/v1/databases";
            string baseUri = $"https://api.notion.com/v1/pages";
            string query = nameof(query);
            string dataBaseKey = "데이터베이스 키 입력";
            string APIKey = "API 키 입력";
            HttpClient client = new();
            Rootobject? parsed = null;

            bool has_more = true;
            //while (has_more)
            //{
            //var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}/{dataBaseKey}/{query}");
            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");
            //request.Content = JsonContent.Create(new QueryFilter { filter = new() { property = "소속", select = new() { equals = "연구소" } } });
            //if (has_more && parsed is not null) request.Content = JsonContent.Create(new QueryOption { page_size = 100, start_cursor = parsed!.next_cursor.ToString()! });
            request.Content = JsonContent.Create(new QueryAdd()
            {
                parent = new() { database_id = dataBaseKey },
                properties = new()
                {
                    소속 = new() { select = new() { name = "연구소" } },
                    이름 = new() { title = [new() { text = new() { content = "Hello" } }] }
                }
            });

            var response = client.Send(request);

            Console.WriteLine(response);
            string utf8 = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
            Console.WriteLine(utf8);

            Console.WriteLine("---------------------------------------------");

            //parsed = JsonSerializer.Deserialize<Rootobject>(utf8);

            //Console.WriteLine(parsed);

            //has_more = parsed!.has_more;
            //}
        }
    }
}