using FastProjects.SharedKernel;

namespace FastProjects.Caching;

/// <summary>
/// Represents a cached query with a specified response type.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;

/// <summary>
/// Represents a cached query with a cache key and expiration time.
/// </summary>
public interface ICachedQuery
{
    /// <summary>
    /// Gets the cache key for the query.
    /// </summary>
    string CacheKey { get; init; }

    /// <summary>
    /// Gets the expiration time for the cache entry.
    /// </summary>
    TimeSpan? Expiration { get; init; }
}
