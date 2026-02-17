using TelegramBot.Domain.Common;

namespace TelegramBot.Domain.Entities
{
    public class Plan : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        public int DurationDays { get; set; }
        public long TrafficMB { get; set; }
        
        public bool IsActive { get; set; } = true;
        public int RemainingCount { get; set; } // Number of accounts remaining to sell

        public int ServerId { get; set; }
        public Server? Server { get; set; }

        public int CategoryId { get; set; }
        public PlanCategory? Category { get; set; }
    }
}
