using MongoDB.Driver;
using MultiTenant.Api.Middlewares;
using Multitenant.Domain.Repositories;
using Multitenant.Infrastructure.Configurations;
using Multitenant.Infrastructure.Persistence;
using Multitenant.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var mongoDbConfiguration = builder.Configuration.GetSection(nameof(MongoDbConfiguration)).Get<MongoDbConfiguration>();
builder.Services.AddSingleton(new MongoDbConfiguration()
{
    ConnectionString = mongoDbConfiguration?.ConnectionString,
    DataBase = mongoDbConfiguration?.DataBase
});
builder.Services.AddScoped<IMongoClient>(ctx => new MongoClient(mongoDbConfiguration?.ConnectionString));
builder.Services.AddScoped(typeof(ApplicationContext));
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(AuthorizationMiddleware));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<AuthorizationMiddleware>();
// app.MapControllers();

app.Run();

