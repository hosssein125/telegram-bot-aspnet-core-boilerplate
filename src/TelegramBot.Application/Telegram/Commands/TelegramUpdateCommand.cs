using MediatR;
using Telegram.Bot.Types;

namespace TelegramBot.Application.Telegram.Commands
{
    public class TelegramUpdateCommand : IRequest
    {
        public Update Update { get; set; } = null!;
    }
}
