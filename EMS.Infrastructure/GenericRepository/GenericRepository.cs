using EMS.Domain.Entities;
using EMS.Domain.Interfaces;
using EMS.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace EMS.Infrastructure.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        private bool _isTransaction;
        private bool _isTransactionDisposed;
        private IDbContextTransaction _transaction;

        public GenericRepository(ApplicationDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public async Task<T> ExecuteRawSQL<T>(string sql) where T : class
        {
            var result = await _dbContext.Set<T>()
                            .FromSqlInterpolated($"{sql}")
                            .FirstOrDefaultAsync();

            return result;
        }

        public async Task<int> ExecuteRawCommand(string command)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync(command);
        }

        public T ExecuteScalar<T>(string sql, params SqlParameter[] parameters)
        {
            var result = _dbContext.Set<RetVal<T>>()
                            .FromSqlRaw(sql, parameters)
                            .ToList();

            return result.Any() ? result.First().Column1 : default;
        }

        public async Task<bool> ExistAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null;
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbSet.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            return await query.AnyAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbSet.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);

            return await query.CountAsync();
        }

        public async Task<TEntity?> FindAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            var query = _dbSet.AsQueryable();

            // Apply the includes if provided
            if (includes != null)
            {
                query = includes(query);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex = 0, int? pageSize = null, string sortField = null, string sortOrder = null, IQueryable<TEntity> dbset = null)
        {
            IQueryable<TEntity> query = dbset ?? _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (pageSize > 0)
            {
                query = query.Skip(pageSize.Value * pageIndex).Take(pageSize.Value); //(pageIndex - 1)
            }
            if (sortField != null && (sortOrder is not null || sortOrder == "asc"))
            {
                if (sortOrder is not null || sortOrder == "asc")
                    query = query.OrderBy(x => sortField);
                if (sortOrder == "desc")
                    query = query.OrderByDescending(x => sortField);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Attach(TEntity entity)
        {
            _dbSet.Attach(entity);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;

            return await _dbContext.SaveChangesAsync(); // entity.Id;
        }

        public async Task<int> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await AddRangeAsync(entities);

            return await _dbContext.SaveChangesAsync();
        }

        public void UpdateState(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task RemoveRange(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await FilterAsync(predicate);
            _dbSet.RemoveRange(entities);
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await RemoveRange(predicate);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteByIdAsync(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            return await DeleteAsync(entityToDelete);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _isTransaction = true;
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync();
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _isTransactionDisposed = true;
        }

        public void Rollback()
        {
            if (_transaction != null && !_isTransactionDisposed)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _isTransactionDisposed = true;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null && !_isTransactionDisposed)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _isTransactionDisposed = true;
            }
        }

        public void Dispose()
        {
            Rollback();
            _dbContext.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await RollbackAsync();
            await _dbContext.DisposeAsync();
        }
    }
}
