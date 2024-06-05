using KakaoChatBotStudy.KakaoAPI;
using KakaoChatBotStudy.Data;
using Microsoft.AspNetCore.Mvc;
using KakaoChatBotStudy.Extension;

namespace KakaoChatBotStudy.Controllers;

[ApiController]
[Route("[controller]")]
public class TextCardTest : ControllerBase
{
    [HttpPost]
    public SkillResponse Post([FromBody] SkillPayload payload)
    {
        Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")}] Text card requested - Request user id: {payload.UserRequest!.User!.Id}");

        SkillResponse response = new();

        try
        {
            var data = payload.Action!.Params!.ToTextCardData();
            var skillTemplate = new SkillTemplate
            {
                Outputs = new()
            };

            TextCard card = new()
            {
                Title = data.Title,
                Description = data.Description,
                Buttons =
                [
                    new ButtonWithWebLink()
                    {
                        Label = data.ButtonLabel,
                        WebLinkUrl = data.ButtonWebLink
                    }
                ]
            };

            skillTemplate.Outputs.AddComponent(card);
            response.Template = skillTemplate;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return response;
    }
}