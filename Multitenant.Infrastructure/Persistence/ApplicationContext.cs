using MongoDB.Driver;
using Multitenant.Domain.Entities;
using Multitenant.Infrastructure.Configurations;

namespace Multitenant.Infrastructure.Persistence;

public class ApplicationContext
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;
    private readonly MongoDbConfiguration _mongoDbConfiguration;

    public ApplicationContext(IMongoClient mongoClient, MongoDbConfiguration mongoDbConfiguration)
    {
        _mongoClient = mongoClient;
        _mongoDbConfiguration = mongoDbConfiguration;
        _mongoDatabase = _mongoClient.GetDatabase(_mongoDbConfiguration?.DataBase);
    }

    public IMongoCollection<Product> Products => _mongoDatabase.GetCollection<Product>("products");
    public IMongoCollection<Tenant> Tenants => _mongoDatabase.GetCollection<Tenant>("Tenants");
}