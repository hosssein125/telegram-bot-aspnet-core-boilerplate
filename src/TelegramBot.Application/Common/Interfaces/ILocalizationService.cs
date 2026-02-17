namespace TelegramBot.Application.Common.Interfaces
{
    public interface ILocalizationService
    {
        Task<string> TranslateAsync(string key);
        Task<string> TranslateAsync(string key, params object[] args);
    }
}

