using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using NSubstitute;

namespace FastProjects.Caching.UnitTests;

public class RedisCacheServiceTests
{
    private readonly IDistributedCache _cacheMock;
    private readonly RedisCacheService _cacheService;

    public RedisCacheServiceTests()
    {
        _cacheMock = Substitute.For<IDistributedCache>();
        _cacheService = new RedisCacheService(_cacheMock);
    }

    [Fact]
    public async Task GetAsync_Should_ReturnCachedValue_WhenKeyExists()
    {
        // Arrange
        const string key = "test_key";
        const int expectedValue = 100;
        byte[] serializedValue = JsonSerializer.SerializeToUtf8Bytes(expectedValue);

        _cacheMock.GetAsync(key, Arg.Any<CancellationToken>()).Returns(serializedValue);

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
        _cacheMock.GetAsync(key, Arg.Any<CancellationToken>()).Returns((byte[]?)null);

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
        byte[] serializedValue = JsonSerializer.SerializeToUtf8Bytes(value);

        // Create the expected options
        var expectedOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        // Act
        await _cacheService.SetAsync(key, value, expiration);

        // Assert
        await _cacheMock.Received(1).SetAsync(key, Arg.Is<byte[]>(b => b.SequenceEqual(serializedValue)),
            Arg.Is<DistributedCacheEntryOptions>(opts => 
                opts.AbsoluteExpirationRelativeToNow == expectedOptions.AbsoluteExpirationRelativeToNow), 
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task RemoveAsync_Should_RemoveValueFromCache()
    {
        // Arrange
        const string key = "test_key";

        // Act
        await _cacheService.RemoveAsync(key);

        // Assert
        await _cacheMock.Received(1).RemoveAsync(key, Arg.Any<CancellationToken>());
    }
}
