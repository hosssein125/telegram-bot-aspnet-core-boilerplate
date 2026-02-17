using TelegramBot.Application.Common.Constants;
using TelegramBot.Application.Common.Models.Telegram;
using TelegramBot.Domain.Entities;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Application.TelegramUsers.Helpers
{
    public static class TelegramUserHelpers
    {
        public static TelegramUser CreateTelegramUser(this TelegramUpdateContext telegramContext)
        {
            return new TelegramUser
            {
                TelegramId = telegramContext.TelegramUserId,
                TelegramUsername = telegramContext.Username,
                FirstName = telegramContext.FirstName,
                LastName = telegramContext.LastName,
                LanguageCode = telegramContext.LanguageCode ?? Defaults.DefaultLanguage,
                State = UserState.MainMenu
            };
        }

    }
}
