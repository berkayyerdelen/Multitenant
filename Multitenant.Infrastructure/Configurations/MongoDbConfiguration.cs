namespace Multitenant.Infrastructure.Configurations;

public class MongoDbConfiguration
{
    public string? ConnectionString { get; set; } = string.Empty;
    public string? DataBase { get; set; }
}