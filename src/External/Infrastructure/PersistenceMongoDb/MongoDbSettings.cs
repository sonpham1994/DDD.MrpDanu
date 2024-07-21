namespace Infrastructure.PersistenceMongoDb;

public sealed class MongoDbSettings
{
    public string ConnectionString { get; init; } = default!;
}