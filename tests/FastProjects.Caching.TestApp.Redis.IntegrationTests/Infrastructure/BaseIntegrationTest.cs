using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FastProjects.Caching.TestApp.Redis.IntegrationTests.Infrastructure;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>   
{
    protected readonly ISender Sender;
    
    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        IServiceScope scope = factory.Services.CreateScope();

        Sender = scope.ServiceProvider.GetRequiredService<ISender>();
    }
}
