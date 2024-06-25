using Microsoft.AspNetCore.Mvc;
using MultiTenant.Api.Responses;
using MultiTenant.Application.Contracts;
using MultiTenant.SharedKernel;

namespace MultiTenant.Api.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult> GetTenantProducts([FromHeader(Name = Constants.Authorization.TenantId)] Guid tenantId, [FromHeader(Name = Constants.Authorization.TenantSecret)] string secret, CancellationToken cancellationToken = default)
    {
        var tenantProducts =await _productService.GetTenantProducts(tenantId, cancellationToken);
        
        return Ok(tenantProducts.Select(x => new TenantProductsApiResponse()
        {
            Name = x.Name,
            EndTime = x.EndTime,
            IsActive = x.IsActive,
            StartTime = x.StartTime,
            UniqueId = x.UniqueId
        }));
       
    }
}