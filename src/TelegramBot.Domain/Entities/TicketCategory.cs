using TelegramBot.Domain.Common;

namespace TelegramBot.Domain.Entities
{
    public class TicketCategory : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
