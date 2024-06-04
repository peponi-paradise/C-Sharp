namespace KakaoChatBotStudy.Extension;

public static class StringExtension
{
    public static string ToFirstLetterLower(this string str)
    {
        return $"{str.ToLower()[0]}{str[1..]}";
    }
}