using MediatR;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramBot.Application.Telegram.Commands;

namespace TelegramBot.Api.Controllers
{
    [ApiController]
    [Route("api/telegram")]
    public class TelegramController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TelegramController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("update")]
        public async Task<IActionResult> ProcessTelegramUpdate([FromBody] Update update)
        {
            await _mediator.Send(new TelegramUpdateCommand { Update = update });
            return Ok();
        }
    }
}
