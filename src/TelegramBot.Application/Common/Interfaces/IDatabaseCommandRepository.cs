using System.Linq.Expressions;
using TelegramBot.Domain.Common;

namespace TelegramBot.Application.Common.Interfaces
{
    public interface IDatabaseCommandRepository<TEntity> where TEntity : BaseEntity
    {

        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void Remove(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
    }
}
