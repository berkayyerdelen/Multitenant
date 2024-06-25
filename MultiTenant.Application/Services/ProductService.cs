using MultiTenant.Application.Contracts;
using MultiTenant.Application.Responses;
using Multitenant.Domain.Repositories;

namespace MultiTenant.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<TenantProductsApplicationResponse>> GetTenantProducts(Guid tenantId, CancellationToken cancellationToken = default)
    {
        var tenantProducts = await _productRepository.GetTenantProducts(tenantId, cancellationToken);

        return tenantProducts.Select(x => new TenantProductsApplicationResponse()
        {
            Name = x.Name,
            EndTime = x.EndTime,
            IsActive = x.IsActive,
            StartTime = x.StartTime,
            UniqueId = x.UniqueId
        });
    }
}