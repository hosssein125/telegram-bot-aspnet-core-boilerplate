using TelegramBot.Application.Common.Constants;
using TelegramBot.Application.Common.Helpers;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Localization;
using TelegramBot.Application.Common.Models.Telegram;
using TelegramBot.Application.TelegramUsers.Helpers;
using TelegramBot.Domain.Entities;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Application.Telegram.Handlers;

public class CustomerMainMenuCommandHandler(IDbUnitOfWork dbUnitOfWork, ITelegramService telegramService, ILocalizationService localization) : ITelegramUpdateHandler
{
    public bool CanHandle(TelegramUser? user, TelegramUpdateContext ctx)
    {
        return user != null && user.Role == UserRole.Customer
                            && ctx is { IsCallback: true, CallbackData: BotCommands.MainMenu };
    }

    public async Task HandleAsync(TelegramUser? user, TelegramUpdateContext telegramContext, CancellationToken cancellationToken)
    {

        // sync the data
        user = telegramContext.SyncTelegramUser();
        await dbUnitOfWork.CommitAsync();

        // Send greeting to customer
        if (user.Role == UserRole.Customer)
            await telegramService.SendInlineMenuAsync(telegramContext.ChatId, await localization.TranslateAsync(TranslationKeys.CaptionGreeting)
                , await MenuBuilder.CreateMainMenu(localization), cancellationToken);

    }

}