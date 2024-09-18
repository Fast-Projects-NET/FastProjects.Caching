using FastProjects.Caching.TestApp.Redis.IntegrationTests.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace FastProjects.Caching.TestApp.Redis.IntegrationTests.CacheService;

public class RedisCacheServiceTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private readonly ICacheService _cacheService = factory.Services.GetRequiredService<ICacheService>();

    [Fact]
    public async Task GetAsync_Should_ReturnCachedValue_WhenKeyExists()
    {
        // Arrange
        const string key = "test_key";
        const int expectedValue = 100;

        await _cacheService.SetAsync(key, expectedValue);

        // Act
        int? result = await _cacheService.GetAsync<int>(key);

        // Assert
        result.Should().Be(expectedValue);
    }
    
    
    [Fact]
    public async Task GetAsync_Should_ReturnDefault_WhenKeyDoesNotExist()
    {
        // Arrange
        const string key = "non_existent_key";

        // Act
        int? result = await _cacheService.GetAsync<int>(key);

        // Assert
        result.Should().Be(default);
    }
    
    [Fact]
    public async Task SetAsync_Should_CacheValue()
    {
        // Arrange
        const string key = "test_key";
        const int value = 100;
        var expiration = TimeSpan.FromMinutes(5);

        // Act
        await _cacheService.SetAsync(key, value, expiration);

        // Assert
        int? result = await _cacheService.GetAsync<int>(key);
        result.Should().Be(value);
    }
    
    [Fact]
    public async Task RemoveAsync_Should_RemoveCachedValue()
    {
        // Arrange
        const string key = "test_key";
        const int value = 100;

        await _cacheService.SetAsync(key, value);

        // Act
        await _cacheService.RemoveAsync(key);

        // Assert
        int? result = await _cacheService.GetAsync<int>(key);
        result.Should().Be(default);
    }
}
