namespace TelegramBot.Application.Common.Interfaces
{
    public interface IIbsNgService
    {
        Task<long> AddAccountAsync(string username, string password, int groupId, double credit, long? firstLogin = null);
        Task<bool> LockAccountAsync(string username, string password);
        Task<bool> UnlockAccountAsync(string username, string password);
        Task<bool> RenewAccountAsync(string username, string password, int groupId, double credit);
        Task<bool> EditAccountAsync(string username, string password, string? newPassword = null, string? newUsername = null);
        Task<bool> GiftUserCreditAndTimeAsync(string username, string password, double credit, int days);
        Task<bool> CheckUserExistsAsync(string username, string password);
        Task<long> GetUserIdAsync(string username, string password);
        Task<object> GetUserInfoAsync(string username, string password);
    }
}
