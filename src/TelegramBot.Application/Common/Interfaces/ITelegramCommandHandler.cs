using TelegramBot.Application.Common.Models.Telegram;
using TelegramBot.Domain.Entities;
namespace TelegramBot.Application.Common.Interfaces
{
    public interface ITelegramUpdateHandler
    {
        bool CanHandle(TelegramUser? user, TelegramUpdateContext context);
        Task HandleAsync(
            TelegramUser? user,
            TelegramUpdateContext telegramContext,
            CancellationToken ct);
    }
}
