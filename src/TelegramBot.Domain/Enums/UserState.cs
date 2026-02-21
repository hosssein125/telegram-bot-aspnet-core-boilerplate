namespace TelegramBot.Domain.Enums
{
    public enum UserState
    {
        MainMenu = 1,
        ProfileMenu = 2,
        SelectPaymentMethod = 3,
        WaitingForPaymentReceipt = 4,
        ViewingPlans = 5,
        SelectingServer = 6,
        WaitingForTicketMessage = 7,
        SupportMenu = 8,
        // Add more states as needed
    }
}
