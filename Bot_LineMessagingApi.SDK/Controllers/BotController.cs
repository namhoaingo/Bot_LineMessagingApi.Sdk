using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_LineMessagingApi.SDK.Extentions;
using Bot_LineMessagingApi.SDK.Models;
using Line.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Bot_LineMessagingApi.SDK.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BotController : Controller
    {
        private readonly BotCredential _botCredential;
        private readonly LineMessagingClient messagingClient;

        public BotController(IOptions<BotCredential> botCredential)
        {
            this._botCredential = botCredential.Value;
            this.messagingClient = new LineMessagingClient(this._botCredential.AppAccessToken);
        }

        [HttpPost]
        [ActionName("callback")]
        public async Task<IActionResult> Post()
        {
            var events = await Request.GetWebhookEventsAsync(_botCredential.AppSecret);
            var app = new LineBotApp(messagingClient);
            await app.RunAsync(events);

            return Ok();
        }

    }
}
