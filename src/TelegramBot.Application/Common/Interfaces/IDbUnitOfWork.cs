using TelegramBot.Application.Common.Interfaces.Repositories;

namespace TelegramBot.Application.Common.Interfaces
{
    public interface IDbUnitOfWork
    {
        IEfTelegramUserRepository TelegramUsers { get; }

        Task<bool> CommitAsync();
        void ClearChangeTracker();
        public (int Added, int Modified, int Deleted) GetPendingChangesCount();

    }
}
