using NotionAPI.Objects;
using System.Net.Http.Json;
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
            Console.WriteLine(Update(baseUri, databaseKey, APIKey));
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

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        static bool Update(string baseUri, string databaseKey, string APIKey)
        {
            // 데이터베이스 정보를 얻어온 후 select에 옵션 추가
            HttpClient client = new();

            // https://developers.notion.com/reference/retrieve-a-database 문서 내용에 따라 Get으로 request 작성
            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/{databaseKey}");
            getRequest.Headers.Add("Authorization", $"Bearer {APIKey}");
            getRequest.Headers.Add("Notion-Version", "2022-06-28");

            var response = client.Send(getRequest);

            // StatusCode를 포함한 HTTP response 출력
            Console.WriteLine(response);
            // JSON 형식의 response data 출력
            var content = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
            Console.WriteLine(content);

            // https://developers.notion.com/reference/update-a-database 문서 내용에 따라 Patch로 request 작성
            var patchRequest = new HttpRequestMessage(HttpMethod.Patch, $"{baseUri}/{databaseKey}");
            patchRequest.Headers.Add("Authorization", $"Bearer {APIKey}");
            patchRequest.Headers.Add("Notion-Version", "2022-06-28");

            // select에 옵션 추가
            var parsed = JsonSerializer.Deserialize<DatabaseInformation>(content);
            if (parsed is not null)
            {
                var information = new DatabaseInformation();
                var property = new Properties();
                property.선택 = new();
                property.선택.select = new();
                var options = new List<Option>();

                foreach (var option in parsed.properties!.선택!.select!.options!)
                {
                    options.Add(new Option() { name = option.name });
                }
                options.Add(new() { name = "3", color = "blue" });

                property.선택!.select!.options = options.ToArray();
                information.properties = property;
                patchRequest.Content = JsonContent.Create(information);
            }

            response = client.Send(patchRequest);

            // StatusCode를 포함한 HTTP response 출력
            Console.WriteLine(response);
            // JSON 형식의 response data 출력
            content = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
            Console.WriteLine(content);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}