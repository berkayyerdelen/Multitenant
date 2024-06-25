using System.Linq.Expressions;
using Multitenant.Domain.Entities;

namespace Multitenant.Domain.Repositories;

public interface ITenantRepository
{
    Task<bool> ValidateAuthorization(Guid tenantId, string secret, CancellationToken cancellationToken = default);
    Task Add(Tenant tenant, CancellationToken cancellationToken = default);
    Task<bool> Any(CancellationToken cancellationToken = default);
}