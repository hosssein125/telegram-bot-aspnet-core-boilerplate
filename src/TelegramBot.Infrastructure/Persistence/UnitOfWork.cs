using Microsoft.EntityFrameworkCore;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Interfaces.Repositories;
using TelegramBot.Infrastructure.Persistence.Repositories;

namespace TelegramBot.Infrastructure.Persistence
{
    public class DbUnitOfWork : IDbUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public DbUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            TelegramUsers = new EfTelegramUserRepository(_context);

        }

        public IEfTelegramUserRepository TelegramUsers { get; }


        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void ClearChangeTracker()
        {
            _context.ChangeTracker.Clear();
        }
        public (int Added, int Modified, int Deleted) GetPendingChangesCount()
        {
            int added = _context.ChangeTracker.Entries()
                .Count(e => e.State == EntityState.Added);

            int modified = _context.ChangeTracker.Entries()
                .Count(e => e.State == EntityState.Modified);

            int deleted = _context.ChangeTracker.Entries()
                .Count(e => e.State == EntityState.Deleted);

            return (added, modified, deleted);
        }

    }
}

