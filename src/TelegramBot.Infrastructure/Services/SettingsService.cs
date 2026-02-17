using Microsoft.EntityFrameworkCore;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Settings;
using TelegramBot.Infrastructure.Persistence;

namespace TelegramBot.Infrastructure.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ApplicationDbContext _context;

        public SettingsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string?> GetAsync(string key)
        {
            var s = await _context.AppSettings.AsNoTracking().FirstOrDefaultAsync(x => x.Key == key);
            return s?.Value;
        }

        public async Task SetAsync(string key, string value)
        {
            var s = await _context.AppSettings.FirstOrDefaultAsync(x => x.Key == key);
            if (s == null)
            {
                _context.AppSettings.Add(new Domain.Entities.AppSetting { Key = key, Value = value });
            }
            else
            {
                s.Value = value;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetLanguageCodeAsync()
        {
            var value = await GetAsync(SettingsKeys.LanguageCode);
            return string.IsNullOrWhiteSpace(value) ? "fa" : value;
        }

        public async Task<long?> GetAdminTelegramIdAsync()
        {
            var value = await GetAsync(SettingsKeys.AdminTelegramId);
            if (long.TryParse(value, out var id))
                return id;
            return null;
        }
    }
}
