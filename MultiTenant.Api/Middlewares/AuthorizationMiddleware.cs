using Multitenant.Domain.Repositories;
using MultiTenant.SharedKernel;

namespace MultiTenant.Api.Middlewares;

public class AuthorizationMiddleware : IMiddleware
{
    private readonly ITenantRepository _tenantRepository;

    public AuthorizationMiddleware(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var hasIdentity = context.Request.Headers.ContainsKey(Constants.Authorization.TenantId) &&
                          context.Request.Headers.ContainsKey(Constants.Authorization.TenantSecret);

        if (!hasIdentity)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("missing");
            return;
        }

        var tenantId = context.Request.Headers[Constants.Authorization.TenantId][0];
        var tenantSecret = context.Request.Headers[Constants.Authorization.TenantSecret][0];
        var isValid = await _tenantRepository.ValidateAuthorization(new Guid(tenantId), tenantSecret);

        if (!isValid)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("un authorized");
            return;
        }
        
        await next(context);
    }
}