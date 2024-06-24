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

    public Task<bool> ValidateAuthorization(Guid tenantId, string secret)
    {
        var filter = Builders<Tenant>.Filter.Eq(x => x.UniqueId, tenantId) &
                     Builders<Tenant>.Filter.Eq(x => x.Secret, secret);
        
        return _applicationContext.Tenants.Find(filter).AnyAsync();
    }
}