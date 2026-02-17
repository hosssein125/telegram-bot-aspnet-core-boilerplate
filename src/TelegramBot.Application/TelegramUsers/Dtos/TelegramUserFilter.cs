using TelegramBot.Domain.Enums;

namespace TelegramBot.Application.TelegramUsers.Dtos
{
    public class TelegramUserFilter
    {
        public long? TelegramId { get; set; }
        public string? TelegramUsername { get; set; }
        public string? FirstName { get; set; }
        public string? FullName { get; set; }
        public string? LastName { get; set; }

        public long? InvitedByUserId { get; set; }
        public decimal? WalletBalanceFrom { get; set; }
        public decimal? WalletBalanceTo { get; set; }
        public UserRole? Role { get; set; }
        public UserState? State { get; set; }
        public bool? IsActive { get; set; }
        public string? ReferralCode { get; set; }
        public int? ServerSwitchNoFrom { get; set; }
        public int? ServerSwitchNoTo { get; set; }
        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }
        public bool? HasPagination { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; }
        public TelegramUserSortBy? SortBy { get; set; }
        public bool? IsDesc { get; set; }
    }
}
