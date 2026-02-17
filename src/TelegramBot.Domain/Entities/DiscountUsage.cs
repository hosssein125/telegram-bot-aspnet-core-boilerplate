using TelegramBot.Domain.Common;

namespace TelegramBot.Domain.Entities
{
    public class DiscountUsage : BaseEntity
    {
        public int UserId { get; set; }
        public TelegramUser? User { get; set; }

        public int DiscountId { get; set; }
        public Discount? Discount { get; set; }

        public DateTime UsedAt { get; set; } = DateTime.UtcNow;
    }
}
