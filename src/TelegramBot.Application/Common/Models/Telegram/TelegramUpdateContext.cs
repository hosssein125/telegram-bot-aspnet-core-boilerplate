using Telegram.Bot.Types;
namespace TelegramBot.Application.Common.Models.Telegram
{
    public sealed class TelegramUpdateContext
    {
        // Identity
        public long TelegramUserId { get; init; }
        public long ChatId { get; init; }

        // User profile (from Telegram)
        public string? Username { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? LanguageCode { get; init; }

        // Input
        public string? MessageText { get; init; }
        public string? CallbackData { get; init; }

        // Type helpers
        public bool IsMessage => MessageText != null;
        public bool IsCallback => CallbackData != null;

        // Command helpers
        public bool IsCommand => IsMessage && MessageText!.StartsWith("/");
        public string? Command =>
            IsCommand ? MessageText!.Split(' ')[0].ToLowerInvariant() : null;

        public static TelegramUpdateContext From(Update update)
        {
            if (update.Message != null)
            {
                var from = update.Message.From!;

                return new TelegramUpdateContext
                {
                    TelegramUserId = from.Id,
                    ChatId = update.Message.Chat.Id,

                    Username = from.Username,
                    FirstName = from.FirstName,
                    LastName = from.LastName,
                    LanguageCode = from.LanguageCode,

                    MessageText = update.Message.Text
                };
            }

            if (update.CallbackQuery != null)
            {
                var from = update.CallbackQuery.From;

                return new TelegramUpdateContext
                {
                    TelegramUserId = from.Id,
                    ChatId = update.CallbackQuery.Message!.Chat.Id,

                    Username = from.Username,
                    FirstName = from.FirstName,
                    LastName = from.LastName,
                    LanguageCode = from.LanguageCode,

                    CallbackData = update.CallbackQuery.Data
                };
            }

            throw new NotSupportedException(
                $"Unsupported Telegram update type: {update.Type}");
        }
    }



}
