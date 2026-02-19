using TelegramBot.Application.Common.Constants;
using TelegramBot.Application.Common.Models.Telegram;
using TelegramBot.Application.Extensions;
using TelegramBot.Domain.Entities;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Application.TelegramUsers.Helpers
{
    public static class TelegramUserHelpers
    {
        public static TelegramUser CreateTelegramCustomerUser(this TelegramUpdateContext telegramContext, long? referrerUserId)
        {
            return new TelegramUser
            {
                TelegramId = telegramContext.TelegramUserId,
                TelegramUsername = telegramContext.Username,
                FirstName = telegramContext.FirstName,
                LastName = telegramContext.LastName,
                LanguageCode = telegramContext.LanguageCode ?? Defaults.DefaultLanguage,
                State = UserState.MainMenu,
                CreatedAt = DateTime.Now,
                IsActive = true,
                WalletBalance = 0,
                ServerSwitchNo = 0,
                ReferralCode = StringExtensions.GenerateRandomId(10, useSpecialCharacters: false),
                Role = UserRole.Customer,
                InvitedByUserId = referrerUserId
            };
        }
        public static TelegramUser SyncTelegramUser(this TelegramUpdateContext telegramContext)
        {
            return new TelegramUser
            {
                TelegramUsername = telegramContext.Username,
                FirstName = telegramContext.FirstName,
                LastName = telegramContext.LastName,
                LanguageCode = telegramContext.LanguageCode ?? Defaults.DefaultLanguage,
                State = UserState.MainMenu,
                UpdatedAt = DateTime.Now,
            };
        }
    }
}
