using TelegramBot.Domain.Common;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Domain.Entities
{
    public class PlanCategory : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        
        public int ServerId { get; set; }
        public Server? Server { get; set; }
        
        public UserRole TargetUserRole { get; set; } = UserRole.Normal; // Vendors or Normal users

        public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    }
}
