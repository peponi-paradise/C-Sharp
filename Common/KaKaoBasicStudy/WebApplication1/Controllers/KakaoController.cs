using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class KakaoController : ControllerBase
    {
        [HttpPost]
        [Route("[controller]")]
        public dynamic Post(dynamic payload)
        {
            Console.WriteLine("Post requested");
            Console.WriteLine(payload);

            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                Dictionary<string, object> text = new Dictionary<string, object>();
                Dictionary<string, object> simpleText = new Dictionary<string, object>();
                List<Dictionary<string, object>> outputs = new List<Dictionary<string, object>>();
                Dictionary<string, object> template = new Dictionary<string, object>();

                text.Add("text", "User");
                simpleText.Add("simpleText", text);
                outputs.Add(simpleText);

                template.Add("outputs", outputs);
                response.Add("version", "2.0");
                response.Add("template", template);
            }
            catch (Exception e)
            {
            }

            return response;
        }
    }

    [ApiController]
    public class DayOffController : ControllerBase
    {
        [HttpPost]
        [Route("[controller]")]
        public dynamic Post(dynamic payload)
        {
            Console.WriteLine("Post requested");
            Console.WriteLine(payload);

            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                Dictionary<string, object> text = new Dictionary<string, object>();
                Dictionary<string, object> simpleText = new Dictionary<string, object>();
                List<Dictionary<string, object>> outputs = new List<Dictionary<string, object>>();
                Dictionary<string, object> template = new Dictionary<string, object>();

                text.Add("text", "DayOff");
                simpleText.Add("simpleText", text);
                outputs.Add(simpleText);

                template.Add("outputs", outputs);
                response.Add("version", "2.0");
                response.Add("template", template);
            }
            catch (Exception e)
            {
            }

            return response;
        }
    }
}