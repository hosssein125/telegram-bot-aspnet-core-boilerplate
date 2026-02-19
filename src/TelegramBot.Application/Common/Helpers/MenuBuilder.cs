using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Application.Common.Constants;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Localization;

namespace TelegramBot.Application.Common.Helpers
{
    public static class MenuBuilder
    {
        public static async Task<InlineKeyboardMarkup> CreateMainMenu(ILocalizationService localization)
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(await localization.TranslateAsync(TranslationKeys.MenuBuy), "menu_buy"),
                    InlineKeyboardButton.WithCallbackData(await localization.TranslateAsync(TranslationKeys.MenuOrders), "menu_orders")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(await localization.TranslateAsync(TranslationKeys.MenuProfile), BotCommands.ProfileMenu),
                    InlineKeyboardButton.WithCallbackData(await localization.TranslateAsync(TranslationKeys.MenuSupport), "menu_support")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(await localization.TranslateAsync(TranslationKeys.MenuHelp), "menu_help")
                }
            });
        }

        public static async Task<InlineKeyboardMarkup> CreateProfileMenu(ILocalizationService localization)
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(await localization.TranslateAsync(TranslationKeys.MenuBackMain), BotCommands.MainMenu),
                    InlineKeyboardButton.WithCallbackData(await localization.TranslateAsync(TranslationKeys.MenuAddBalance), BotCommands.AddBalanceMenu)
                }
            });
        }

        public static async Task<InlineKeyboardMarkup> CreateAdminMenu(ILocalizationService L)
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(await L.TranslateAsync(TranslationKeys.AdminUsers), "admin_users"),
                    InlineKeyboardButton.WithCallbackData(await L.TranslateAsync(TranslationKeys.AdminServers), "admin_servers")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(await L.TranslateAsync(TranslationKeys.AdminPlans), "admin_plans"),
                    InlineKeyboardButton.WithCallbackData(await L.TranslateAsync(TranslationKeys.AdminTickets), "admin_tickets")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(await L.TranslateAsync(TranslationKeys.MenuBackMain), "menu_main")
                }
            });
        }

        public static async Task<InlineKeyboardMarkup> CreatePaymentMethodsMenu(ILocalizationService L)
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(await L.TranslateAsync(TranslationKeys.PayCard), "pay_card"),
                    InlineKeyboardButton.WithCallbackData(await L.TranslateAsync(TranslationKeys.PayCrypto), "pay_crypto")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(await L.TranslateAsync(TranslationKeys.MenuBack), "menu_wallet")
                }
            });
        }

        public static async Task<InlineKeyboardMarkup> CreateBackOnly(ILocalizationService L)
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(await L.TranslateAsync(TranslationKeys.MenuBack), "back")
                }
            });
        }
    }
}
