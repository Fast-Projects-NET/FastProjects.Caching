using Microsoft.Extensions.Caching.Memory;

namespace FastProjects.Caching;

/// <summary>
/// Provides an in-memory cache service.
/// </summary>
/// <param name="cache">The memory cache instance.</param>
public class InMemoryCacheService(IMemoryCache cache) : ICacheService
{
    /// <inheritdoc />
    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        return cache.TryGetValue(key, out T value)
            ? Task.FromResult(value)
            : Task.FromResult<T?>(default);
    }

    /// <inheritdoc />
    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        cache.Set(key, value, cacheEntryOptions);

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        cache.Remove(key);
        return Task.CompletedTask;
    }
}
