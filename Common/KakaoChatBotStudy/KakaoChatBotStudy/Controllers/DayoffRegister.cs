using KakaoChatBotStudy.Extension;
using KakaoChatBotStudy.KakaoAPI;
using Microsoft.AspNetCore.Mvc;

namespace KakaoChatBotStudy.Controllers;

[ApiController]
[Route("[controller]")]
public class DayoffRegister : ControllerBase
{
    [HttpPost]
    public SkillResponse Post([FromBody] SkillPayload payload)
    {
        Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")}] Dayoff register requested - Request user id: {payload.UserRequest!.User!.Id}");

        SkillResponse response = new();

        try
        {
            var registerData = payload.Action!.Params!.ToDayoffRegisterData();
            var skillTemplate = new SkillTemplate
            {
                Outputs = new()
            };

            TextCard card = new TextCard();
            card.Title = "Register dayoff";
            card.Description = $"Success to register.{Environment.NewLine}" +
                $"1. Date: {registerData.Date.ToString("yyyy-MM-dd")}{Environment.NewLine}" +
                $"2. Type: {registerData.Type}";

            card.Buttons = new();
            ButtonWithWebLink button = new();
            button.Label = "Download draft";
            button.WebLinkUrl = "http://192.168.124.171:80/Dayoff/연차Template.xlsx";
            card.Buttons.Add(button);

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