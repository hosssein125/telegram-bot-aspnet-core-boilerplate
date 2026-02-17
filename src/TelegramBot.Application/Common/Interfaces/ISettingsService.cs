using System.Threading.Tasks;

namespace TelegramBot.Application.Common.Interfaces
{
    public interface ISettingsService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, string value);
        Task<string> GetLanguageCodeAsync();
        Task<long?> GetAdminTelegramIdAsync();
    }
}
