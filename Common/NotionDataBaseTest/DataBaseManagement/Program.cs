using NotionAPI.Objects;
using System.Text.Json;

namespace DatabaseManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string baseUri = $"https://api.notion.com/v1/databases";
            string databaseKey = "데이터베이스 키";
            string APIKey = "API 키";

            Console.WriteLine(Retrieve(baseUri, databaseKey, APIKey));
        }

        static bool Retrieve(string baseUri, string databaseKey, string APIKey)
        {
            HttpClient client = new();

            // https://developers.notion.com/reference/retrieve-a-database 문서 내용에 따라 Get으로 request 작성
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/{databaseKey}");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");

            var response = client.Send(request);

            // StatusCode를 포함한 HTTP response 출력
            Console.WriteLine(response);
            // JSON 형식의 response data 출력
            var content = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
            Console.WriteLine(content);

            var parsed = JsonSerializer.Deserialize<DatabaseInformation>(content);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}