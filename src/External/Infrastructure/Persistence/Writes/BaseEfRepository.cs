using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.SharedKernel.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Infrastructure.Persistence.Writes;

internal abstract class BaseEfRepository<T> where T : AggregateRoot
{
    protected readonly DbSet<T> dbSet;
    protected readonly AppDbContext context;
    protected BaseEfRepository(AppDbContext context)
    {
        this.context = context;
        dbSet = context.Set<T>();
    }

    protected virtual async ValueTask<T?> GetByIdAsync(object id, CancellationToken cancellationToken)
    {
        return await dbSet.FindAsync(new object?[] { id, cancellationToken }, cancellationToken: cancellationToken);
    }

    public virtual void Save(T entity)
    {
        /* For Detached Entity (Disconnected Entity / New Entity) - Add new Case
         * Attach: 
         *  - When objects are detached, if Id is default (e.g 0, Guid.Empty), it will mark as Added, otherwise
         *  Unchanged
         *  - It just update the modified value of properties inside of DbContext
         * Update:
         *  - When objects are detached, if Id is default (e.g 0, Guid.Empty), it will mark as Added, otherwise
         *  Modified
         *  - It will update all value of properties regardless of modifying or not.
         * Add:
         *  - When objects are detached, it mark them as Added, otherwise Unchanged.
         */

        /* For Non-Detached Entity (Connected Entity / Entity exists in DbContext) - Update Case
         * Update:
         *  - Update all value of properties regardless of none of them are changed. 
         *  - If reassigning the internal entity:
         *      + Detached object: the internal entity will update all value of properties regardless of modifying or not
         *      + Attached object: the internal entity will update the modified value of properties
         *  
         * Attach or dont' call any method:
         *  - Just update the modified value of properties. 
         *  - If reassigning the internal entity:
         *      + Detached object: the internal entity will update all value of properties regardless of modifying or not
         *      + Attached object: the internal entity will update the modified value of properties
         *  
         */
        dbSet.Attach(entity);
    }

    protected async Task BulkDeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await dbSet.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
    }
}

internal abstract class BaseEfGuidStronglyTypedIdRepository<T, TId>
    where T : AggregateRootGuidStronglyTypedId<TId>
    where TId : struct, IEquatable<TId>, IGuidStronglyTypedId
{
    protected readonly DbSet<T> dbSet;
    protected readonly AppDbContext context;
    protected BaseEfGuidStronglyTypedIdRepository(AppDbContext context)
    {
        this.context = context;
        dbSet = context.Set<T>();
    }

    protected virtual async ValueTask<T?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return await dbSet.FindAsync(new object?[] { id, cancellationToken }, cancellationToken: cancellationToken);
    }

    public virtual void Save(T entity)
    {
        /* For Detached Entity (Disconnected Entity / New Entity) - Add new Case
         * Attach: 
         *  - When objects are detached, if Id is default (e.g 0, Guid.Empty), it will mark as Added, otherwise
         *  Unchanged
         *  - It just update the modified value of properties inside of DbContext
         * Update:
         *  - When objects are detached, if Id is default (e.g 0, Guid.Empty), it will mark as Added, otherwise
         *  Modified
         *  - It will update all value of properties regardless of modifying or not.
         * Add:
         *  - When objects are detached, it mark them as Added, otherwise Unchanged.
         */

        /* For Non-Detached Entity (Connected Entity / Entity exists in DbContext) - Update Case
         * Update:
         *  - Update all value of properties regardless of none of them are changed. 
         *  - If reassigning the internal entity:
         *      + Detached object: the internal entity will update all value of properties regardless of modifying or not
         *      + Attached object: the internal entity will update the modified value of properties
         *  
         * Attach or dont' call any method:
         *  - Just update the modified value of properties. 
         *  - If reassigning the internal entity:
         *      + Detached object: the internal entity will update all value of properties regardless of modifying or not
         *      + Attached object: the internal entity will update the modified value of properties
         *  
         */
        dbSet.Attach(entity);
    }

    //protected async Task BulkDeleteAsync(TId id, CancellationToken cancellationToken)
    //{
    //    await dbSet.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
    //}
}
