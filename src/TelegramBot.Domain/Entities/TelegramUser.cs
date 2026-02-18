using TelegramBot.Domain.Common;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Domain.Entities
{
    public class TelegramUser : BaseEntity
    {
        public long TelegramId { get; set; }
        public string? TelegramUsername { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public long? InvitedByUserId { get; set; }
        public decimal WalletBalance { get; set; }
        public UserRole Role { get; set; } = UserRole.Customer;

        public UserState State { get; set; } = UserState.MainMenu;
        public string StateData { get; set; } = "{}"; // JSON for temporary state data
        public bool IsActive { get; set; }
        public string? ReferralCode { get; set; }
        public int ServerSwitchNo { get; set; }
        public string LanguageCode { get; set; } = "fa";

        public TelegramUser? InvitedByUser { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<PaymentHistory> Payments { get; set; } = new List<PaymentHistory>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<DiscountUsage> DiscountUsages { get; set; } = new List<DiscountUsage>();

    }
}
