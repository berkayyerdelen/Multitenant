namespace MultiTenant.Api.Responses;

public class TenantProductsApiResponse
{
    public Guid UniqueId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}