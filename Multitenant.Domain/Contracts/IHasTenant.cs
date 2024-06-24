namespace Multitenant.Domain.Contracts;

public interface IHasTenant
{
    public Guid? TenantId { get; set; }
}