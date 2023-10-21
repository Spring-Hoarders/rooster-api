using System.Linq.Expressions;

namespace Rooster.Application.Common.Common.Data;

public interface IDataWriter
{
    IDataWriter Add<TEntity>(TEntity entity) where TEntity : class;
    IDataWriter Remove<TEntity>(TEntity entity) where TEntity : class;
    IDataWriter Update<TEntity>(TEntity entity) where TEntity : class;
    Task UpsertAsync<TEntity>(TEntity entity) where TEntity : class;
    Task UpsertRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    IDataWriter AddRange<TEntity>(IList<TEntity> entities) where TEntity : class;
    IDataWriter RemoveRange<TEntity>(IList<TEntity> entities) where TEntity : class;
    
    Task RemoveAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    Task<IList<TEntity>> UpdateAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Action<TEntity> action)
        where TEntity : class;
    Task<TEntity> UpdateOneAsync<TEntity>(Action<TEntity> action, params object[] id) where TEntity : class;
    Task InTransactionAsync(Func<Task> code);
    Task SaveAsync(CancellationToken cancellationToken = default);
    void Save();
}