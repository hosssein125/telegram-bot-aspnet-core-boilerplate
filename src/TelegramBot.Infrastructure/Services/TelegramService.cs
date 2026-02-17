using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Application.Common.Interfaces;

namespace TelegramBot.Infrastructure.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly ITelegramBotClient _bot;

        public TelegramService(ITelegramBotClient bot)
        {
            _bot = bot;
        }

        // 1️⃣ Send simple text message (Async suffix removed, now SendMessage)
        public async Task SendTextAsync(long chatId, string text, CancellationToken ct)
            => await _bot.SendMessage(chatId, text, cancellationToken: ct);

        // 2️⃣ Send text with inline keyboard
        public async Task SendInlineMenuAsync(
            long chatId, string? caption, InlineKeyboardMarkup keyboard, CancellationToken ct)
        {
            await _bot.SendMessage(
                chatId: chatId,
                text: caption ?? "",
                replyMarkup: keyboard,
                cancellationToken: ct);
        }

        // 3️⃣ Send text with reply keyboard
        public async Task SendReplyMenuAsync(
            long chatId, string? caption, ReplyKeyboardMarkup keyboard,
            bool oneTime = false, CancellationToken ct = default)
        {
            keyboard.OneTimeKeyboard = oneTime;

            await _bot.SendMessage(
                chatId: chatId,
                text: caption ?? "",
                replyMarkup: keyboard,
                cancellationToken: ct);
        }

        // 4️⃣ Force reply
        public async Task SendForceReplyAsync(long chatId, string? caption, CancellationToken ct)
        {
            await _bot.SendMessage(
                chatId: chatId,
                text: caption ?? "",
                replyMarkup: new ForceReplyMarkup(),
                cancellationToken: ct);
        }

        // 5️⃣ Send sticker (InputOnlineFile is gone, use InputFile)
        public async Task SendStickerAsync(long chatId, string stickerFileIdOrUrl, CancellationToken ct)
        {
            await _bot.SendSticker(
                chatId: chatId,
                sticker: InputFile.FromString(stickerFileIdOrUrl),
                cancellationToken: ct);
        }

        // 6️⃣ Send photo
        public async Task SendPhotoAsync(long chatId, string photoFileIdOrUrl, string? caption, CancellationToken ct)
        {
            await _bot.SendPhoto(
                chatId: chatId,
                photo: InputFile.FromString(photoFileIdOrUrl),
                caption: caption,
                cancellationToken: ct);
        }

        // 7️⃣ Send video
        public async Task SendVideoAsync(long chatId, string videoFileIdOrUrl, string? caption, CancellationToken ct)
        {
            await _bot.SendVideo(
                chatId: chatId,
                video: InputFile.FromString(videoFileIdOrUrl),
                caption: caption,
                cancellationToken: ct);
        }

        // 8️⃣ Send emoji
        public async Task SendEmojiAsync(long chatId, string emoji, CancellationToken ct)
            => await SendTextAsync(chatId, emoji, ct);

        // 9️⃣ Request user phone number
        public async Task RequestPhoneNumberAsync(long chatId, string text, CancellationToken ct)
        {
            var keyboard = new ReplyKeyboardMarkup(
                new[] { KeyboardButton.WithRequestContact(text) })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };

            await _bot.SendMessage(chatId, text, replyMarkup: keyboard, cancellationToken: ct);
        }

        // 🔟 Request user location
        public async Task RequestLocationAsync(long chatId, string text, CancellationToken ct)
        {
            var keyboard = new ReplyKeyboardMarkup(
                new[] { KeyboardButton.WithRequestLocation(text) })
            {
                ResizeKeyboard = true,
                OneTimeKeyboard = true
            };

            await _bot.SendMessage(chatId, text, replyMarkup: keyboard, cancellationToken: ct);
        }

        // 1️⃣1️⃣ Delete a message
        public async Task DeleteMessageAsync(long chatId, int messageId, CancellationToken ct)
            => await _bot.DeleteMessage(chatId, messageId, cancellationToken: ct);

        // 1️⃣2️⃣ Edit message text
        public async Task EditMessageTextAsync(
            long chatId, int messageId, string newText,
            InlineKeyboardMarkup? keyboard, CancellationToken ct)
        {
            await _bot.EditMessageText(
                chatId: chatId,
                messageId: messageId,
                text: newText,
                replyMarkup: keyboard,
                cancellationToken: ct);
        }
    }
}