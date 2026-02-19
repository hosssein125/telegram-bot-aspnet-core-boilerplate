using TelegramBot.Application.Common.Constants;
using TelegramBot.Application.Common.Helpers;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Localization;
using TelegramBot.Application.Common.Models.Telegram;
using TelegramBot.Application.Extensions;
using TelegramBot.Application.TelegramUsers.Dtos;
using TelegramBot.Application.TelegramUsers.Helpers;
using TelegramBot.Domain.Entities;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Application.Telegram.Handlers
{
    public class CustomerStartCommandHandler(IDbUnitOfWork dbUnitOfWork, ITelegramService telegramService, ILocalizationService localization) : ITelegramUpdateHandler
    {
        public bool CanHandle(TelegramUser? user, TelegramUpdateContext ctx)
        {
            return (user == null || (user.Role != UserRole.Agent && user.Role != UserRole.Admin)) && (ctx.MessageText == BotCommands.Start
                                                                 || ctx is { IsCallback: true, CallbackData: BotCommands.Start });
        }

        public async Task HandleAsync(TelegramUser? user, TelegramUpdateContext telegramContext, CancellationToken cancellationToken)
        {
            //Add user to db if it is the first visit
            if (user == null)
            {
                // Extract the referrer user if the user joins using a referral code.
                var referrerCode = telegramContext.MessageText?.GetStartReferral();
                var referrer = referrerCode == null ? null :
                   await dbUnitOfWork.TelegramUsers.GetByReferralCodeAsync(referrerCode, new TelegramUserIncludes(), cancellationToken);

                user = telegramContext.CreateTelegramCustomerUser(referrer?.Id);
                dbUnitOfWork.TelegramUsers.Add(user);
                await dbUnitOfWork.CommitAsync();
            }
            // if user was registered sync the data
            else
            {
                user = telegramContext.SyncTelegramUser();
                await dbUnitOfWork.CommitAsync();
            }

            // Send greeting to customer
            if (user.Role == UserRole.Customer)
                await telegramService.SendInlineMenuAsync(telegramContext.ChatId, await localization.TranslateAsync(TranslationKeys.Greeting)
                    , await MenuBuilder.CreateMainMenu(localization), cancellationToken);

        }

    }
}
