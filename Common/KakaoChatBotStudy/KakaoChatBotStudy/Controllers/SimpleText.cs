using KakaoChatBotStudy.KakaoAPI;
using KakaoChatBotStudy.Data;
using Microsoft.AspNetCore.Mvc;
using KakaoChatBotStudy.Extension;

namespace KakaoChatBotStudy.Controllers;

[ApiController]
[Route("[controller]")]
public class SimpleTextTest : ControllerBase
{
    [HttpPost]
    public SkillResponse Post([FromBody] SkillPayload payload)
    {
        Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")}] Simple text requested - Request user id: {payload.UserRequest!.User!.Id}");

        SkillResponse response = new();

        try
        {
            var data = payload.Action!.Params!.ToSimpleTextData();
            var skillTemplate = new SkillTemplate
            {
                Outputs = []
            };

            SimpleText text = new() { Text = data.Text };

            skillTemplate.Outputs.AddComponent(text);
            response.Template = skillTemplate;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return response;
    }
}