using TelegramBot.Domain.Common;

namespace TelegramBot.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public TelegramUser? User { get; set; }

        public int ServerId { get; set; }
        public Server? Server { get; set; }
        
        public int? PlanId { get; set; } // Optional: Link to the plan purchased
        public Plan? Plan { get; set; }

        // IBSng specific
        public string? VpnUsername { get; set; }
        public string? VpnPassword { get; set; }

        // 3x-ui specific
        public string? ClientUuid { get; set; }
    }
}
