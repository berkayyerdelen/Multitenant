using MongoDB.Driver;
using Multitenant.Domain.Entities;
using Multitenant.Domain.Repositories;
using Multitenant.Infrastructure.Persistence;

namespace Multitenant.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationContext _applicationContext;

    public ProductRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public Task<Product> Get(Guid uniqueId, CancellationToken cancellationToken = default)
    {
        var filter = new FilterDefinitionBuilder<Product>().Eq(x => x.UniqueId, uniqueId);
        return _applicationContext.Product.FindSync(filter, new FindOptions<Product>(), cancellationToken).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task Add(Product product, CancellationToken cancellationToken = default)
    {
        await _applicationContext.Product.InsertOneAsync(product,new InsertOneOptions(), cancellationToken);
    }

    public Task<List<Product>> GetTenantProducts(Guid tenantId, CancellationToken cancellationToken = default)
    {
        var filter = new FilterDefinitionBuilder<Product>().Eq(x => x.TenantId, tenantId);
        return _applicationContext.Product.FindSync(filter, new FindOptions<Product>(), cancellationToken).ToListAsync(cancellationToken);
    }
}