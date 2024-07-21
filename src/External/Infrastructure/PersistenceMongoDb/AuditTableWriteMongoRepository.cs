using Application.Interfaces.Writes.AuditTrailWrite;
using Domain.AuditTrail.SharedKernel;
using MongoDB.Driver;

namespace Infrastructure.PersistenceMongoDb;

internal class AuditTableWriteMongoRepository<T>(IMongoDatabase database, string collectionName) 
    : IAuditTableWriteRepository<T>
    where T : AuditEntity
{
    private readonly IMongoCollection<T> dbCollection = database.GetCollection<T>(collectionName);

    public async Task InsertAsync(T entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(entity));
    
        await dbCollection.InsertOneAsync(entity);
    }
}