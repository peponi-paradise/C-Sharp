using KakaoChatBotStudy.KakaoAPI;

namespace KakaoChatBotStudy.Extension;

public static class SkillTemplateExtension
{
    public static void AddComponent(this List<Dictionary<string, Component>> outputs, Component component)
    {
        outputs?.Add(new() { { component.GetType().Name.ToFirstLetterLower(), component } });
    }
}