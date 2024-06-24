namespace Multitenant.Domain.Entities;

public class Tenant
{
    public Guid UniqueId { get; set; }
    public string Secret { get; set; }
}