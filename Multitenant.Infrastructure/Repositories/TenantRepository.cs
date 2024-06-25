using System.Linq.Expressions;
using MongoDB.Driver;
using Multitenant.Domain.Entities;
using Multitenant.Domain.Repositories;
using Multitenant.Infrastructure.Persistence;

namespace Multitenant.Infrastructure.Repositories;

public class TenantRepository : ITenantRepository
{
    private readonly ApplicationContext _applicationContext;

    public TenantRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public Task<bool> ValidateAuthorization(Guid tenantId, string secret, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Tenant>.Filter.Eq(x => x.UniqueId, tenantId) &
                     Builders<Tenant>.Filter.Eq(x => x.Secret, secret);

        return _applicationContext.Tenants.Find(filter).AnyAsync();
    }

    public async Task Add(Tenant tenant, CancellationToken cancellationToken = default)
    {
        await _applicationContext.Tenants.InsertOneAsync(tenant, new InsertOneOptions(), cancellationToken);
    }

    public Task<bool> Any(CancellationToken cancellationToken = default)
    {
        return _applicationContext.Tenants.Find(x => true).AnyAsync(cancellationToken);
    }
}