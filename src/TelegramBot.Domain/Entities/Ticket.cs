using TelegramBot.Domain.Common;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public int UserId { get; set; }
        public TelegramUser? User { get; set; }

        public string Title { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public TicketStatus Status { get; set; } = TicketStatus.Open;

        public TicketCategory Category { get; set; }
        public ICollection<TicketMessage> Messages { get; set; } = new List<TicketMessage>();
    }
}
