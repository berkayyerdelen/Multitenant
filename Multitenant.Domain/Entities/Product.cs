using Multitenant.Domain.Contracts;

namespace Multitenant.Domain.Entities;

public class Product : IHasTenant
{
    public Guid UniqueId { get; set; }
    public string Name { get; set; }
    public Guid? TenantId { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

