using Bogus;
using Multitenant.Domain.Entities;
using Multitenant.Domain.Repositories;

namespace MultiTenant.Api.Configurations;

public class TenantSeeder : IStartupFilter
{
    private readonly IServiceProvider _serviceProvider;

    public TenantSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            
            using (var scope = _serviceProvider.CreateScope())
            {   
                
                var _tenantRepository = scope.ServiceProvider.GetRequiredService<ITenantRepository>();
                var hasAnyTenant =  _tenantRepository.Any().GetAwaiter().GetResult();

                var tenantsFaker = new Faker<Tenant>()
                    .RuleFor(x=> x.UniqueId , f=> Guid.NewGuid())
                    .RuleFor(x=>x.Secret, y=>y.Hacker.Noun());
                var tenants = tenantsFaker.Generate(10);
                
                if (!hasAnyTenant)
                {
                  
                    
                    foreach (var tenant in tenants)
                    {
                        _tenantRepository.Add(tenant, CancellationToken.None);
                    }
                }

                var _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

                var hasAnyProduct = _productRepository.Any(CancellationToken.None).GetAwaiter().GetResult();

                if (!hasAnyProduct)
                {
                    var productsFaker = new Faker<Product>()
                        .RuleFor(x => x.UniqueId, f => Guid.NewGuid())
                        .RuleFor(x => x.TenantId, y => tenants.First().UniqueId)
                        .RuleFor(x => x.Name, y => y.Hacker.Noun())
                        .RuleFor(x => x.IsActive, true)
                        .RuleFor(x => x.StartTime, DateTime.UtcNow)
                        .RuleFor(x => x.EndTime, DateTime.UtcNow.AddDays(10));
                        
                    var products = productsFaker.Generate(10);

                    foreach (var product in products)
                    {

                        _productRepository.Add(product, CancellationToken.None);
                    }
                }

            }

            next(app);
        };
    }
}