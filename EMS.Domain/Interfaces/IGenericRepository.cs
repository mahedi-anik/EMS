using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace EMS.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<int> ExecuteRawCommand(string command);
        Task<T> ExecuteRawSQL<T>(string sql) where T : class;
        T ExecuteScalar<T>(string sql, params SqlParameter[] parameters);

        Task<bool> ExistAsync(object id);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FindAsync(object id);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

        Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex = 0, int? pageSize = null, string sortField = null, string sortOrder = null, IQueryable<TEntity> dbset = null);
        Task<IEnumerable<TEntity>> GetAllAsync();

        void Add(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<int> InsertAsync(TEntity entity);
        Task<int> InsertRangeAsync(IEnumerable<TEntity> entities);

        void UpdateState(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);

        void Remove(TEntity entity);
        Task RemoveRange(Expression<Func<TEntity, bool>> predicate);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> DeleteByIdAsync(object id);
        Task SaveChangesAsync();

        void Attach(TEntity entity);
    }
}
