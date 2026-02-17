using TelegramBot.Domain.Common;

namespace TelegramBot.Domain.Entities
{
    public class AppSetting : BaseEntity
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}

