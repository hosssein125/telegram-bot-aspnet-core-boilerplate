using TelegramBot.Application.TelegramUsers.Dtos;
using TelegramBot.Domain.Entities;

namespace TelegramBot.Application.Common.Interfaces.Repositories
{
    public interface IEfTelegramUserRepository : IDatabaseCommandRepository<TelegramUser>
    {
        Task<TelegramUser?> GetByIdAsync(int id, TelegramUserIncludes includes, CancellationToken cancellationToken);
        Task<TelegramUser?> GetByReferralCodeAsync(string referralCode, TelegramUserIncludes includes, CancellationToken cancellationToken);
        Task<List<TelegramUser>> GetByIdsAsync(List<int> ids, TelegramUserIncludes includes, CancellationToken cancellationToken);
        Task<TelegramUser?> GetByTelegramIdAsync(long telegramId, TelegramUserIncludes includes, CancellationToken cancellationToken);
        Task<List<TelegramUser>> GetByTelegramIdsAsync(List<long> telegramIds, TelegramUserIncludes includes,
            CancellationToken cancellationToken);
        Task<List<TelegramUser>> GetByFilterAsync(TelegramUserFilter filter, TelegramUserIncludes includes,
            CancellationToken cancellationToken);
    }
}
