using TelegramBot.Application.Common.Constants;
using TelegramBot.Application.Common.Helpers;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Localization;
using TelegramBot.Application.Common.Models.Telegram;
using TelegramBot.Application.TelegramUsers.Helpers;
using TelegramBot.Domain.Entities;

namespace TelegramBot.Application.Telegram.Handlers
{
    public class StartCommandHandler(IDbUnitOfWork dbUnitOfWork, ITelegramService telegramService, ILocalizationService localization) : ITelegramUpdateHandler
    {
        public bool CanHandle(TelegramUser user, TelegramUpdateContext ctx)
        {
            return ctx.MessageText == BotCommands.Start
                   || ctx is { IsCallback: true, CallbackData: BotCommands.Start };
        }

        public async Task HandleAsync(TelegramUser? user, TelegramUpdateContext telegramContext, CancellationToken cancellationToken)
        {
            //Add user to db if it is the first visit
            if (user == null)
            {
                user = telegramContext.CreateTelegramUser();
                dbUnitOfWork.TelegramUsers.Add(user);
                await dbUnitOfWork.CommitAsync();
            }

            // Send greeting
            await telegramService.SendInlineMenuAsync(telegramContext.ChatId, await localization.TranslateAsync(TranslationKeys.Greeting)
                , await MenuBuilder.CreateMainMenu(localization), cancellationToken);
        }

    }
}
