using System.Linq.Expressions;

namespace Concert.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> All();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
