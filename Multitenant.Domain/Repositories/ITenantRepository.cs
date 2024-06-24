namespace Multitenant.Domain.Repositories;

public interface ITenantRepository
{
    Task<bool> ValidateAuthorization(Guid tenantId, string secret);
}