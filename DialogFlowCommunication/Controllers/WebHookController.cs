using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiAiSdkNetCore.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DialogFlowCommunication.Controllers
{

    public class WebHookController : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            using (var streamReader = new StreamReader(Request.Body))
            {
                AIResponse response = JsonConvert.DeserializeObject<AIResponse>(streamReader.ReadToEnd());
            }

            return Json("Web Hook Controller Index");
        }
    }
}