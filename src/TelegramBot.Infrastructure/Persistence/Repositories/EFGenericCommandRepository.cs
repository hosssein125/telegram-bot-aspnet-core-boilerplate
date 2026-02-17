using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TelegramBot.Application.Common.Interfaces;
using TelegramBot.Domain.Common;

namespace TelegramBot.Infrastructure.Persistence.Repositories
{
    public class EFGenericCommandRepository<TEntity> : IDatabaseCommandRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext DbContext;

        protected EFGenericCommandRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        #region Queries

        public async Task<bool> ExistsAsync(int id)
        {
            return await DbContext.Set<TEntity>().Where(x => x.Id == id).AnyAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbContext.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        #endregion

        #region Commands

        public void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            DbContext.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            DbContext.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            DbContext.Set<TEntity>().UpdateRange(entities);
        }

        #endregion
    }
}