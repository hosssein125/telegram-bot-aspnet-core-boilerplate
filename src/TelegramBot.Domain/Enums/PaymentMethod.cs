namespace TelegramBot.Domain.Enums
{
    public enum PaymentMethod
    {
        Receipt = 1,     // Photo of payment receipt (Card to Card)
        BankGateway = 2, // Online Bank Gateway
        Wallet = 3,      // User's internal wallet
        Crypto = 4       // Cryptocurrency
    }
}
