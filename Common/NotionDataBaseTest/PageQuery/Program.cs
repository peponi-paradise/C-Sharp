namespace PageQuery
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string baseUri = $"https://api.notion.com/v1/pages";
        }

        private static bool Create(string baseUri, string APIKey)
        {
            HttpClient client = new();

            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}");
            request.Headers.Add("Authorization", $"Bearer {APIKey}");
            request.Headers.Add("Notion-Version", "2022-06-28");

            var response = client.Send(request);

            // StatusCode를 포함한 HTTP response 출력
            Console.WriteLine(response);
            // JSON 형식의 response data 출력
            Console.WriteLine(new StreamReader(response.Content.ReadAsStream()).ReadToEnd());

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}