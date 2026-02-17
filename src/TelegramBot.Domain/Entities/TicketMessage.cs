using TelegramBot.Domain.Common;

namespace TelegramBot.Domain.Entities
{
    public class TicketMessage : BaseEntity
    {
        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; }

        public string Message { get; set; } = string.Empty;
        public bool IsAdminReply { get; set; } = false; // True if reply from admin, False if from user
    }
}
