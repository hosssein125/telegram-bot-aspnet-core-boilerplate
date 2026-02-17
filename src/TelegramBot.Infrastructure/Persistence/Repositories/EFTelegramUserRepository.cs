using Microsoft.EntityFrameworkCore;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Application.Common.Interfaces.Repositories;
using TelegramBot.Application.TelegramUsers.Dtos;
using TelegramBot.Domain.Entities;
using TelegramBot.Infrastructure.Persistence.Extensions;

namespace TelegramBot.Infrastructure.Persistence.Repositories
{
    public class EfTelegramUserRepository : EFGenericCommandRepository<TelegramUser>,
     IEfTelegramUserRepository
    {

        private readonly IQueryable<TelegramUser> _queryable;
        private readonly IApplicationDbContext _context;

        public EfTelegramUserRepository(ApplicationDbContext context) : base(context)
        {
            _queryable = DbContext.Set<TelegramUser>();
            _context = context;
        }

        public async Task<TelegramUser?> GetByIdAsync(int id, TelegramUserIncludes includes, CancellationToken cancellationToken)
        {

            var query = _queryable;

            query = query.ApplyIncludes(includes);

            // Get From Database
            return await query
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
        }

        public async Task<TelegramUser?> GetByReferralCodeAsync(string referralCode, TelegramUserIncludes includes, CancellationToken cancellationToken)
        {

            var query = _queryable;

            query = query.ApplyIncludes(includes);

            // Get From Database
            return await query
                .SingleOrDefaultAsync(x => x.ReferralCode == referralCode, cancellationToken: cancellationToken);
        }


        public async Task<List<TelegramUser>> GetByIdsAsync(List<int> ids, TelegramUserIncludes includes, CancellationToken cancellationToken)
        {
            var query = _queryable;

            if (ids?.Any() == true)
            {
                query = query.Where(x => ids.Contains(x.Id));
            }

            query = query.ApplyIncludes(includes);

            // Get From Database
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TelegramUser?> GetByTelegramIdAsync(long telegramId, TelegramUserIncludes includes, CancellationToken cancellationToken)
        {

            var query = _queryable;

            query = query.ApplyIncludes(includes);

            // Get From Database
            return await query
                .SingleOrDefaultAsync(x => x.TelegramId == telegramId, cancellationToken: cancellationToken);
        }

        public async Task<List<TelegramUser>> GetByTelegramIdsAsync(List<long> telegramIds, TelegramUserIncludes includes, CancellationToken cancellationToken)
        {
            var query = _queryable;

            if (telegramIds?.Any() == true)
            {
                query = query.Where(x => telegramIds.Contains(x.TelegramId));
            }

            query = query.ApplyIncludes(includes);

            // Get From Database
            return await query.ToListAsync(cancellationToken);
        }


        public async Task<List<TelegramUser>> GetByFilterAsync(TelegramUserFilter filter, TelegramUserIncludes includes, CancellationToken cancellationToken)
        {
            var query = _queryable;

            query = query.ApplyIncludes(includes);
            query = query.ApplyFilter(filter);
            query = query.ApplySort(filter.SortBy, filter.IsDesc);

            // Get From Database
            return await query.ToListAsync(cancellationToken);
        }


    }
}
