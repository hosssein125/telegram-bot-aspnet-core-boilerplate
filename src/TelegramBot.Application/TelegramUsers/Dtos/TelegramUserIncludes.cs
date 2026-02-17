namespace TelegramBot.Application.TelegramUsers.Dtos
{
    public class TelegramUserIncludes
    {
        public bool InvitedByUser { get; set; } = false;
        public bool Tickets { get; set; } = false;
        public bool DiscountUsages { get; set; } = false;
        public bool Payments { get; set; } = false;
        public bool Orders { get; set; } = false;

    }
}
