using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;

namespace FastProjects.Caching.UnitTests;

public class InMemoryCacheServiceTests
{
    private readonly IMemoryCache _cacheMock;
    private readonly InMemoryCacheService _cacheService;

    public InMemoryCacheServiceTests()
    {
        _cacheMock = Substitute.For<IMemoryCache>();
        _cacheService = new InMemoryCacheService(_cacheMock);
    }

    [Fact]
    public async Task GetAsync_Should_ReturnCachedValue_WhenKeyExists()
    {
        // Arrange
        const string key = "test_key";
        const int expectedValue = 100;

        _cacheMock.TryGetValue(key, out Arg.Any<object?>()).Returns(x =>
        {
            x[1] = expectedValue;
            return true;
        });

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
        _cacheMock.TryGetValue(key, out Arg.Any<object?>()).Returns(false);

        // Act
        int? result = await _cacheService.GetAsync<int>(key);

        // Assert
        result.Should().Be(0);
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
        _cacheMock.Received(1).Set(key, value);
    }


    [Fact]
    public async Task RemoveAsync_Should_RemoveValueFromCache()
    {
        // Arrange
        const string key = "test_key";

        // Act
        await _cacheService.RemoveAsync(key);

        // Assert
        _cacheMock.Received(1).Remove(key);
    }
}
