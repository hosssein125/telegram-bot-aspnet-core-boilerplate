using TelegramBot.Domain.Common;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Domain.Entities
{
    public class Discount : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public DiscountType Type { get; set; } // Percentage or Amount
        public decimal Value { get; set; } // The percentage or amount value

        public DateTime? ExpireDate { get; set; }
        public int TotalCount { get; set; }
        public int RemainingCount { get; set; }

        public bool IsActive { get; set; } = true;
        public int MaxUsagePerUser { get; set; } = 1;

        public int? PlanId { get; set; }
        public Plan? Plan { get; set; }

        public UserRole TargetUserRole { get; set; } = UserRole.Customer;

        public ICollection<DiscountUsage> Usages { get; set; } = new List<DiscountUsage>();
    }
}
