using MultiTenant.Application.Responses;

namespace MultiTenant.Application.Contracts;

public interface IProductService
{
    Task<IEnumerable<TenantProductsApplicationResponse>> GetTenantProducts(Guid tenantId, CancellationToken cancellationToken = default);
}