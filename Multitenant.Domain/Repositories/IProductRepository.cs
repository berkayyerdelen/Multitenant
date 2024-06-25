using Multitenant.Domain.Entities;

namespace Multitenant.Domain.Repositories;

public interface IProductRepository
{
    Task<Product> Get(Guid uniqueId, CancellationToken cancellationToken = default);
    Task Add(Product product, CancellationToken cancellationToken =default);
    Task<List<Product>> GetTenantProducts(Guid tenantId, CancellationToken cancellationToken = default);
    Task<bool> Any(CancellationToken cancellationToken = default);
}