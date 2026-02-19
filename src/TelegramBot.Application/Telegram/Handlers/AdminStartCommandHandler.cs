using TelegramBot.Application.Common.Constants;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Models.Telegram;
using TelegramBot.Application.TelegramUsers.Helpers;
using TelegramBot.Domain.Entities;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Application.Telegram.Handlers;

public class AdminStartCommandHandler(IDbUnitOfWork dbUnitOfWork, ITelegramService telegramService, ILocalizationService localization) : ITelegramUpdateHandler
{
    public bool CanHandle(TelegramUser? user, TelegramUpdateContext ctx)
    {
        return user != null && user.Role == UserRole.Admin && ctx.MessageText == BotCommands.Start
               || ctx is { IsCallback: true, CallbackData: BotCommands.Start };
    }

    public async Task HandleAsync(TelegramUser? user, TelegramUpdateContext telegramContext, CancellationToken cancellationToken)
    {

        // sync the data
        user = telegramContext.SyncTelegramUser();
        await dbUnitOfWork.CommitAsync();

        // ToDo: Send greeting to admin
    }

}