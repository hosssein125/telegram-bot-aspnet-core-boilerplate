using TelegramBot.Domain.Common;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Domain.Entities
{
    public class Server : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string PanelUrl { get; set; } = string.Empty;
        
        // 3x-ui credentials
        public string? Username { get; set; }
        public string? Password { get; set; }
        
        // IBSng/AnyConnect token
        public string? Token { get; set; }
        
        public PanelType PanelType { get; set; }
        public string ProtocolType { get; set; } = string.Empty; // e.g. "vless", "vmess"
        
        // JSON configuration for this specific inbound/server
        public string InboundConfigJson { get; set; } = "{}";

        public ICollection<Plan> Plans { get; set; } = new List<Plan>();
        public ICollection<PlanCategory> PlanCategories { get; set; } = new List<PlanCategory>();
    }
}
