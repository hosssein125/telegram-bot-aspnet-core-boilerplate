using TelegramBot.Domain.Common;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public int UserId { get; set; }
        public TelegramUser? User { get; set; }

        public string Title { get; set; } = string.Empty;
        public TicketStatus Status { get; set; } = TicketStatus.Open;

        public ICollection<TicketMessage> Messages { get; set; } = new List<TicketMessage>();
    }
}
