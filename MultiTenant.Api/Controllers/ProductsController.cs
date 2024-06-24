using Microsoft.AspNetCore.Mvc;
using MultiTenant.SharedKernel;

namespace MultiTenant.Api.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetTenantProducts([FromHeader(Name = Constants.Authorization.TenantId)] Guid tenantId, [FromHeader(Name = Constants.Authorization.TenantSecret)] string secret, CancellationToken cancellationToken = default)
    {   
        return Ok();
    }
}