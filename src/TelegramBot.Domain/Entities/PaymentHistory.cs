using TelegramBot.Domain.Common;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Domain.Entities
{
    public class PaymentHistory : BaseEntity
    {
        public int UserId { get; set; }
        public TelegramUser? User { get; set; }

        public int? PlanId { get; set; }
        public Plan? Plan { get; set; }

        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public string? TransactionId { get; set; }
        
        public PaymentState State { get; set; } = PaymentState.Pending;
        public PaymentMethod Method { get; set; } = PaymentMethod.Receipt;
        
        public string? ReceiptImageFileId { get; set; } // Telegram File ID for the receipt image
    }
}
