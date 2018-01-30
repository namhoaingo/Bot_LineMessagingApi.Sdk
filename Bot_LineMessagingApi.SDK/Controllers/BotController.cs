using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot_LineMessagingApi.SDK.Extentions;
using Bot_LineMessagingApi.SDK.Managers;
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
        private readonly ITokenManager _tokenManager;
        private LineMessagingClient _messagingClient;

        public BotController(IOptions<BotCredential> botCredential, ITokenManager tokenManager)
        {
            this._tokenManager = tokenManager;
            this._botCredential = botCredential.Value;            
        }

        [HttpPost]
        [ActionName("callback")]
        public async Task<IActionResult> Post()
        {
            AccessToken accessToken = await _tokenManager.GetAccessTokenFromCache().ConfigureAwait(false);
            _messagingClient = new LineMessagingClient(accessToken.access_token);

            var events = await Request.GetWebhookEventsAsync(_botCredential.AppSecret);
            var app = new LineBotApp(_messagingClient);
            await app.RunAsync(events);

            return Ok();
        }

    }
}
