using KakaoChatBotStudy.Extension;

namespace KakaoChatBotStudy.Data;

public static class UserInputData
{
    public static SimpleTextData ToSimpleTextData(this Dictionary<string, string> dic)
    {
        return new()
        {
            Text = dic[nameof(SimpleTextData.Text).ToFirstLetterLower()]
        };
    }

    public static TextCardData ToTextCardData(this Dictionary<string, string> dic)
    {
        return new()
        {
            Title = dic[nameof(TextCardData.Title).ToFirstLetterLower()],
            Description = dic[nameof(TextCardData.Description).ToFirstLetterLower()],
            ButtonLabel = dic[nameof(TextCardData.ButtonLabel).ToFirstLetterLower()],
            ButtonWebLink = dic[nameof(TextCardData.ButtonWebLink).ToFirstLetterLower()]
        };
    }
}