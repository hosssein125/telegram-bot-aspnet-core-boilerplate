namespace TelegramBot.Application.Common.Interfaces
{
    using global::Telegram.Bot.Types.ReplyMarkups;

    public interface ITelegramService
    {
        // 1. Simple text message
        Task SendTextAsync(long chatId, string text, CancellationToken ct);

        // 2. Text with inline keyboard
        Task SendInlineMenuAsync(
            long chatId,
            string? caption,
            InlineKeyboardMarkup keyboard,
            CancellationToken ct);

        // 3. Text with reply keyboard (under input field)
        Task SendReplyMenuAsync(
            long chatId,
            string? caption,
            ReplyKeyboardMarkup keyboard,
            bool oneTime = false,
            CancellationToken ct = default);

        // 4. Force reply (prompt)
        Task SendForceReplyAsync(
            long chatId,
            string? caption,
            CancellationToken ct);

        // 5. Send sticker
        Task SendStickerAsync(long chatId, string stickerFileId, CancellationToken ct);

        // 6. Send image / photo
        Task SendPhotoAsync(long chatId, string photoFileIdOrUrl, string? caption, CancellationToken ct);

        // 7. Send video
        Task SendVideoAsync(long chatId, string videoFileIdOrUrl, string? caption, CancellationToken ct);

        // 8. Send emoji (wrapper for SendTextAsync)
        Task SendEmojiAsync(long chatId, string emoji, CancellationToken ct);

        // 9. Request user contact (phone number)
        Task RequestPhoneNumberAsync(long chatId, string text, CancellationToken ct);

        // 10. Request location
        Task RequestLocationAsync(long chatId, string text, CancellationToken ct);

        // 11. Delete a message
        Task DeleteMessageAsync(long chatId, int messageId, CancellationToken ct);

        // 12. Edit message text (for inline buttons)
        Task EditMessageTextAsync(
            long chatId,
            int messageId,
            string newText,
            InlineKeyboardMarkup? keyboard,
            CancellationToken ct);
    }
}
