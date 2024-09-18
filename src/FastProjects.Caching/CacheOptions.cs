using Microsoft.Extensions.Caching.Distributed;

namespace FastProjects.Caching;

/// <summary>
/// Provides methods to create cache entry options with specified expiration times.
/// </summary>
internal static class CacheOptions
{
    /// <summary>
    /// Gets the default expiration options for cache entries.
    /// </summary>
    private static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
    };

    /// <summary>
    /// Creates cache entry options with a specified expiration time.
    /// </summary>
    /// <param name="expiration">The expiration time for the cache entry. If null, the default expiration is used.</param>
    /// <returns>The cache entry options with the specified or default expiration time.</returns>
    public static DistributedCacheEntryOptions Create(TimeSpan? expiration) =>
        expiration is not null ?
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration } :
            DefaultExpiration;
}
