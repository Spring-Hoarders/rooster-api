using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Rooster.Application.Common.Common.Data;
using Rooster.Application.Common.Common.Exceptions;
using Rooster.Domain.Common;

namespace Rooster.Infrastructure.Persistence.Common.Data;

public class EfDataWriter : IDataWriter
{
    private readonly AppDbContext _db;
    private readonly List<object> _addEntities = new();
    private readonly List<object> _updateEntities = new();
    private readonly List<object> _deleteEntities = new();
    private IDbContextTransaction _transaction;
    private int _transactionRefCount = 0;

    public EfDataWriter(AppDbContext db)
    {
        _db = db;
    }

    private void AddOrReplaceToList(List<object> list, object entity)
    {
        var existingEntity = list.Find(c => c.Equals(entity));
        if (existingEntity != null)
            existingEntity = entity;
        else
            list.Add(entity);
    }

    public IDataWriter Add<TEntity>(TEntity entity) where TEntity : class
    {
        AddOrReplaceToList(_addEntities, entity);
        return this;
    }

    public IDataWriter Remove<TEntity>(TEntity entity) where TEntity : class
    {
        AddOrReplaceToList(_deleteEntities, entity);
        return this;
    }

    public IDataWriter Update<TEntity>(TEntity entity) where TEntity : class
    {
        AddOrReplaceToList(_updateEntities, entity);
        return this;
    }

    public async Task UpsertAsync<TEntity>(TEntity entity) where TEntity : class
    {
        var exists = await _db.Set<TEntity>().FirstOrDefaultAsync(e => e.Equals(entity));
        var addToList = exists == null ? _addEntities : _updateEntities;
        AddOrReplaceToList(addToList, entity);
    }

    public async Task UpsertRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        foreach (var e in entities)
        {
            await UpsertAsync(e);
        }
    }

    public IDataWriter AddRange<TEntity>(IList<TEntity> entities) where TEntity : class
    {
        foreach (var entity in entities)
            AddOrReplaceToList(_addEntities, entity);

        return this;
    }

    public IDataWriter RemoveRange<TEntity>(IList<TEntity> entities) where TEntity : class
    {
        foreach (var entity in entities)
            AddOrReplaceToList(_deleteEntities, entity);

        return this;
    }

    public async Task RemoveAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        var result = await _db.Set<TEntity>().Where(predicate).ToListAsync();
        foreach (var entity in result)
            AddOrReplaceToList(_deleteEntities, entity);
    }

    public async Task<IList<TEntity>> UpdateAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Action<TEntity> action)
        where TEntity : class
    {
        var result = await _db.Set<TEntity>().Where(predicate).ToListAsync();
        foreach (var entity in result)
        {
            action.Invoke(entity);
            AddOrReplaceToList(_updateEntities, entity);
        }

        return result;
    }
    
    public async Task<TEntity> GetForUpdateAsync<TEntity>(params object[] id) where TEntity : class
    {
        var result = await _db.Set<TEntity>().FindAsync(id);
        return result ??
               throw new EntityNotFoundException(nameof(TEntity), id.ToString());
    }

    public async Task<TEntity> UpdateOneAsync<TEntity>(Action<TEntity> action, params object[] id) where TEntity : class
    {
        var result = await GetForUpdateAsync<TEntity>(id);
        action.Invoke(result);
        AddOrReplaceToList(_updateEntities, result);
        return result;
    }

    public async Task InTransactionAsync(Func<Task> code)
    {
        if (_db.Database.CurrentTransaction == null)
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        _transactionRefCount += 1;
        try
        {
            await code();

            _transactionRefCount -= 1;

            if (_transaction != null && _transactionRefCount == 0)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
                _transactionRefCount = 0;
            }
        }
        catch (Exception)
        {
            if (_transaction == null) throw;

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
            _transactionRefCount = 0;
            throw;
        }
    }
    
    private void ClearDbContext()
    {
        _addEntities.Clear();
        _updateEntities.Clear();
        _deleteEntities.Clear();

        var entityEntries = _db.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added ||
                        e.State == EntityState.Modified ||
                        e.State == EntityState.Unchanged ||
                        e.State == EntityState.Deleted)
            .ToList();

        foreach (var item in entityEntries)
            item.State = EntityState.Detached;
    }

    private void UpdateDbContext()
    {
        foreach (var item in _addEntities)
        {
            _db.Entry(item).State = EntityState.Added;
        }

        foreach (var item in _updateEntities)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        foreach (var item in _deleteEntities)
            _db.Entry(item).State = EntityState.Deleted;
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        UpdateDbContext();

        await _db.SaveChangesAsync(cancellationToken);

        ClearDbContext();
    }

    public void Save()
    {
        UpdateDbContext();

        _db.SaveChanges();

        ClearDbContext();
    }
}