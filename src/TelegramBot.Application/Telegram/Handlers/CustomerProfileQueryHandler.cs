using TelegramBot.Application.Common.Constants;
using TelegramBot.Application.Common.Helpers;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Localization;
using TelegramBot.Application.Common.Models.Telegram;
using TelegramBot.Domain.Entities;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Application.Telegram.Handlers
{

    public class CustomerProfileQueryHandler(IDbUnitOfWork dbUnitOfWork, ITelegramService telegramService, ILocalizationService localization) : ITelegramUpdateHandler
    {
        public bool CanHandle(TelegramUser? user, TelegramUpdateContext ctx)
        {
            return user?.Role == UserRole.Customer && ctx is { IsCallback: true, CallbackData: BotCommands.ProfileMenu };
        }

        public async Task HandleAsync(TelegramUser? user, TelegramUpdateContext telegramContext, CancellationToken cancellationToken)
        {

            // Set State To Profile Menu
            user.State = UserState.ProfileMenu;
            await dbUnitOfWork.CommitAsync();

            // Create Profile Caption
            var profileCaption = await localization.TranslateAsync(TranslationKeys.CaptionCustomerProfile,
                [user.FirstName, user.LastName, user.TelegramId, user.TelegramUsername, user.WalletBalance, user.Orders.Count, DateTime.Now.Subtract(user.CreatedAt).Days]);

            // Send profile to customer
            if (user.Role == UserRole.Customer)
                await telegramService.SendInlineMenuAsync(telegramContext.ChatId, profileCaption
                    , await MenuBuilder.CreateProfileMenu(localization), cancellationToken);


        }

    }
    public class SupportQueryHandler(IDbUnitOfWork dbUnitOfWork, ITelegramService telegramService, ILocalizationService localization) : ITelegramUpdateHandler
    {
        public bool CanHandle(TelegramUser? user, TelegramUpdateContext ctx)
        {
            return user?.Role != UserRole.Admin && ctx is { IsCallback: true, CallbackData: BotCommands.SupportMenu };
        }

        public async Task HandleAsync(TelegramUser? user, TelegramUpdateContext telegramContext, CancellationToken cancellationToken)
        {

            // Set State To Profile Menu
            user.State = UserState.SupportMenu;
            await dbUnitOfWork.CommitAsync();

            // Create support Caption
            var supportCaption = await localization.TranslateAsync(TranslationKeys.CaptionSupport);

            // Send profile to customer
            if (user.Role == UserRole.Customer)
                await telegramService.SendInlineMenuAsync(telegramContext.ChatId, supportCaption
                    , await MenuBuilder.CreateSupportMenu(localization), cancellationToken);


        }

    }
}
