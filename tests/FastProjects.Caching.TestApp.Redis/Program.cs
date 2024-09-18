using FastEndpoints;
using FastEndpoints.Swagger;
using FastProjects.Caching;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// FastEndpoints
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.DocumentName = "Initial Release";
        s.Title = "FastProjects.Data.EntityFrameworkCore Test API";
        s.Description = "API to test the FastProjects.Data.EntityFrameworkCore library";
        s.Version = "v0";
    };
});

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);

    cfg.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
});

// Caching (Redis)
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
});
builder.Services.AddTransient<ICacheService, RedisCacheService>();

WebApplication app = builder.Build();

// Endpoints and swagger
app.MapFastEndpoints();
app.UseSwaggerGen();

app.Run();

public partial class Program;
