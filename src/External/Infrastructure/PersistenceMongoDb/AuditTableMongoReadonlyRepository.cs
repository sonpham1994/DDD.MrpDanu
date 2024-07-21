using System.Linq.Expressions;
using Infrastructure.Persistence.Externals.AuditTables.EntitiesAudit.Base;
using MongoDB.Driver;

namespace Infrastructure.PersistenceMongoDb;

internal class AuditTableMongoReadonlyRepository<T>(IMongoDatabase database, string collectionName)
    where T : EntityAudit
{
    private readonly IMongoCollection<T> dbCollection = database.GetCollection<T>(collectionName);
    private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
    }
    
    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await dbCollection.Find(filter).ToListAsync();
    }
}