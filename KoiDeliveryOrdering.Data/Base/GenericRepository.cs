// using System.Linq.Expressions;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Query;
//

using System.Linq.Expressions;
using KoiDeliveryOrdering.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace KoiDeliveryOrdering.Data.Base;

public class GenericRepository<TEntity> where TEntity : class
{
    private readonly KoiDeliveryOrderingDbContext _dbContext;
    protected DbSet<TEntity> _dbSet;

    public GenericRepository(KoiDeliveryOrderingDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    protected KoiDeliveryOrderingDbContext DbContext
    {
        get
        {
            if (_dbContext == null!) return new KoiDeliveryOrderingDbContext();
            return _dbContext;
        }
    }

    #region Retrieve operations

    public TEntity? Find(params object[] keyValues)
    {
        var entity = _dbSet.Find(keyValues);
        if(entity == null) return null;

        if (_dbSet.Entry(entity).State == EntityState.Added)
        {
            _dbSet.Entry(entity).State = EntityState.Detached;
        }

        return entity;

        //return _dbSet.Find(keyValues);
    }

    public IEnumerable<TEntity> FindAll()
    {
        var entities = _dbSet.ToList();

        foreach (var entity in entities)
        {
            _dbSet.Entry(entity).State = EntityState.Detached;
        }

        return entities;
        //return _dbSet.ToList(); 
    }

    public async Task<TEntity?> FindAsync(params object[] keyValues)
    {
        var entity = await _dbSet.FindAsync(keyValues);
        if (entity == null) return null;

        _dbSet.Entry(entity).State = EntityState.Detached;

        return entity;

        //return await _dbSet.FindAsync(keyValues);
    }

    public virtual async Task<IEnumerable<TEntity>> FindAllAsync()
    {
        var entities = await _dbSet.ToListAsync();

        foreach (var entity in entities)
        {
            _dbSet.Entry(entity).State = EntityState.Detached;
        }

        return entities;

        //return await _dbSet.ToListAsync();
    }

    public virtual async Task<TEntity?> FindOneWithConditionAsync(
        Expression<Func<TEntity, bool>>? filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split(
                         new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);

                // Add AsSplitQuery when includes are present
                query = query.AsSplitQuery();
            }
        }

        TEntity? result;
        if (orderBy != null)
            result = await orderBy(query).FirstOrDefaultAsync();
        else
            result = await query.FirstOrDefaultAsync();

        if (result == null) return null;

        //_dbSet.Entry(result).State = EntityState.Detached;
        return result;
    }

    public async Task<IEnumerable<TEntity>> FindAllWithConditionAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties.Split(
                         new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);

                // Add AsSplitQuery when includes are present
                query = query.AsSplitQuery();
            }
        }

        IEnumerable<TEntity> result;
        if (orderBy != null)
            result = await orderBy(query).ToListAsync();
        else       
            result = await query.ToListAsync();

        if(!result.Any()) return new List<TEntity>();

        foreach (var entity in result)
        {
            _dbSet.Entry(entity).State = EntityState.Detached;
        }

        return result;
    }

    public async Task<IEnumerable<TEntity>> FindAllWithConditionAndThenIncludeAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>? includes = null)
    {
        IQueryable<TEntity> query = _dbSet.AsQueryable();

        if (filter != null)
            query = query.Where(filter);

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = include(query);

                // Add AsSplitQuery when includes are present
                query = query.AsSplitQuery();
            }
        }

        IEnumerable<TEntity> result;
        if (orderBy != null)
            result = await orderBy(query).ToListAsync();
        else
            result = await query.ToListAsync();

        if (!result.Any()) return new List<TEntity>();

        foreach (var entity in result)
        {
            _dbSet.Entry(entity).State = EntityState.Detached;
        }

        return result;
    }

    #endregion
    
    #region Insert/Update/Remove operations
    
    public void PrepareInsert(TEntity entity)
    {
        _dbContext.Add(entity);
    }
    public async Task PrepareInsertAsync(TEntity entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
        _dbContext.SaveChanges();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

  
    public void PrepareRemove(object id)
    {
        var entityToDelete = _dbSet.Find(id);
        if (entityToDelete != null) PerformRemove(entityToDelete);
    }
    public async Task PrepareRemoveAsync(object id)
    {
        var entityToDelete = await _dbSet.FindAsync(id);
        if (entityToDelete != null) PerformRemove(entityToDelete);
    }
    private void PerformRemove(TEntity entityToDelete)
    {
        if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }

        _dbSet.Remove(entityToDelete);
    }

    public void PrepareUpdate(TEntity entity)
    {
        var tracker = _dbContext.Attach(entity);
        tracker.State = EntityState.Modified;
    }
    public async Task UpdateAsync(TEntity entityToUpdate, bool saveChanges = false)
    {
        _dbSet.Attach(entityToUpdate);

        // Set the entity state to Modified
        _dbContext.Entry(entityToUpdate).State = EntityState.Modified;

        // Get primary key property name 
        var keyProperties = _dbContext.Model.FindEntityType(typeof(TEntity))?.GetProperties();

        // Handle identity columns and primary keys
        if (keyProperties != null)
        {
            foreach (var property in keyProperties)
            {
                var propertyEntry = _dbContext.Entry(entityToUpdate).Property(property.Name);

                // Exclude primary key properties from being modified
                if (property.IsKey())
                {
                    propertyEntry.IsModified = false;
                }

                // Exclude identity columns (auto-increment) from being modified
                if (property.ValueGenerated == Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd)
                {
                    propertyEntry.IsModified = false;
                }
            }
        }

        if (saveChanges) await SaveChangeAsync();
    }

    #endregion

    #region Save operations

    public async Task<int> SaveChangeAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public int SaveChangeWithTransaction()
    {
        int result = -1;

        using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                result = _dbContext.SaveChanges();
                dbContextTransaction.Commit();
            }
            catch (Exception)
            {
                result = -1;
                _dbContext.Database.RollbackTransaction();
            }
        }

        return result;
    }

    public async Task<int> SaveChangeWithTransactionAsync()
    {
        int result = -1;

        using (var dbContextTransaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                result = await _dbContext.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
            }
            catch (Exception)
            {
                result = -1;
                await _dbContext.Database.RollbackTransactionAsync();
            }
        }

        return result;
    }

    #endregion
}