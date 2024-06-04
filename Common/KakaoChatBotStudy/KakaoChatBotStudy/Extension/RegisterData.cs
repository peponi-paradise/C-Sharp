using KakaoChatBotStudy.Data;

namespace KakaoChatBotStudy.Extension;

public static class RegisterDataExtension
{
    public static UserRegisterData ToUserRegisterData(this Dictionary<string, string> dic)
    {
        return new()
        {
            NotionName = dic[nameof(UserRegisterData.NotionName).ToFirstLetterLower()],
            Organization = dic[nameof(UserRegisterData.Organization).ToFirstLetterLower()],
            Password = dic[nameof(UserRegisterData.Password).ToFirstLetterLower()]
        };
    }

    public static DayoffRegisterData ToDayoffRegisterData(this Dictionary<string, string> dic)
    {
        string originalDateValue = dic[nameof(DayoffRegisterData.Date).ToLower()];
        string extractValue = originalDateValue.Replace("{", "").Replace("}", "").Replace("\\", "").Replace("\"", "");
        extractValue = extractValue.Split(',')[1].Split(':')[1];
        DateTime date = DateTime.Parse(extractValue);
        return new()
        {
            Date = date,
            Type = dic[nameof(DayoffRegisterData.Type).ToLower()]
        };
    }
}