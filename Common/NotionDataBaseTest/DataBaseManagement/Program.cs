using NotionAPI.Objects;
using System.Net.Http.Json;
using System.Text.Json;

namespace DatabaseManagement
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string baseUri = "https://api.notion.com/v1/databases";
            string databaseKey = "데이터베이스 키";
            string APIKey = "API 키";

            Console.WriteLine(Retrieve(baseUri, databaseKey, APIKey));
            Console.WriteLine(Update(baseUri, databaseKey, APIKey));
            Console.WriteLine(RetrieveAndUpdate(baseUri, databaseKey, APIKey));
        }

        private static bool Retrieve(string baseUri, string databaseKey, string APIKey)
        {
            HttpClient client = new();

            // https://developers.notion.com/reference/retrieve-a-database 문서 내용에 따라 Get으로 request 작성
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/{databaseKey}");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");

            var response = client.Send(request);

            // StatusCode를 포함한 Header 출력
            Console.WriteLine(response);
            // JSON 형식의 Body 출력
            var content = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
            Console.WriteLine(content);

            // Parsing
            var parsed = JsonSerializer.Deserialize<DatabaseInformation>(content);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private static bool Update(string baseUri, string databaseKey, string APIKey)
        {
            HttpClient client = new();

            // https://developers.notion.com/reference/update-a-database 문서 내용에 따라 Patch로 request 작성
            var patchRequest = new HttpRequestMessage(HttpMethod.Patch, $"{baseUri}/{databaseKey}");
            patchRequest.Headers.Add("Authorization", $"Bearer {APIKey}");
            patchRequest.Headers.Add("Notion-Version", "2022-06-28");

            // select에 '3' 옵션 추가
            var information = new DatabaseInformation();
            var property = new Dictionary<string, DatabaseProperty>();
            var select = new DatabaseSelect();
            property.Add("선택", select);
            select.select = new()
            {
                options = new()
            };

            // 기존 옵션 정보 추가 (덮어쓰기 형태로 동작)
            for (int index = 1; index < 3; index++)
            {
                select.select.options.Add(new() { name = index.ToString() });
            }
            select.select.options.Add(new() { name = "3", color = "blue" });

            information.properties = property;
            patchRequest.Content = JsonContent.Create(information);

            var response = client.Send(patchRequest);

            // StatusCode를 포함한 Header 출력
            Console.WriteLine(response);
            // JSON 형식의 Body 출력
            var content = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
            Console.WriteLine(content);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        private static bool RetrieveAndUpdate(string baseUri, string databaseKey, string APIKey)
        {
            // 기존 select 정보를 얻기 위해 데이터베이스 정보를 얻어옴
            HttpClient client = new();

            // https://developers.notion.com/reference/retrieve-a-database 문서 내용에 따라 Get으로 request 작성
            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/{databaseKey}");
            getRequest.Headers.Add("Authorization", $"Bearer {APIKey}");
            getRequest.Headers.Add("Notion-Version", "2022-06-28");

            var response = client.Send(getRequest);
            var content = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();

            // https://developers.notion.com/reference/update-a-database 문서 내용에 따라 Patch로 request 작성
            var patchRequest = new HttpRequestMessage(HttpMethod.Patch, $"{baseUri}/{databaseKey}");
            patchRequest.Headers.Add("Authorization", $"Bearer {APIKey}");
            patchRequest.Headers.Add("Notion-Version", "2022-06-28");

            // Parsing 후 select에 '3' 옵션 추가
            var parsed = JsonSerializer.Deserialize<DatabaseInformation>(content);
            if (parsed is not null)
            {
                var information = new DatabaseInformation();
                var property = new Dictionary<string, DatabaseProperty>();
                var select = new DatabaseSelect();
                property.Add("선택", select);
                select.select = new()
                {
                    options = new()
                };

                foreach (var option in ((DatabaseSelect)parsed.properties!["선택"]!).select!.options!)
                {
                    select.select.options.Add(new Select() { name = option.name });
                }
                select.select.options.Add(new() { name = "3", color = "blue" });

                information.properties = property;
                patchRequest.Content = JsonContent.Create(information);
            }

            response = client.Send(patchRequest);

            // StatusCode를 포함한 Header 출력
            Console.WriteLine(response);
            // JSON 형식의 Body 출력
            content = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
            Console.WriteLine(content);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}