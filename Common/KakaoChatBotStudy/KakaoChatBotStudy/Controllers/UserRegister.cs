using KakaoChatBotStudy.KakaoAPI;
using KakaoChatBotStudy.Data;
using Microsoft.AspNetCore.Mvc;
using KakaoChatBotStudy.Extension;

namespace KakaoChatBotStudy.Controllers;

[ApiController]
[Route("[controller]")]
public class UserRegister : ControllerBase
{
    [HttpPost]
    public SkillResponse Post([FromBody] SkillPayload payload)
    {
        Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")}] User register requested - Request user id: {payload.UserRequest!.User!.Id}");

        SkillResponse response = new();

        try
        {
            var registerData = payload.Action!.Params!.ToUserRegisterData();
            var skillTemplate = new SkillTemplate
            {
                Outputs = new()
            };

            bool isRightUser = registerData.Password == UserRegisterData.GivenPassword;
            string result = isRightUser == true ? "Success" : "Failed";
            result = $"{result} to register user{Environment.NewLine}" +
                $"1. Notion name: {registerData.NotionName}{Environment.NewLine}" +
                $"2. Organization: {registerData.Organization}";

            SimpleText text = new() { Text = result };

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