namespace FastProjects.Caching;

/// <summary>
/// Defines methods for caching operations.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Retrieves a cached item asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the cached item.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous get operation. The task result contains the cached item, or null if the item is not found.</returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds or updates a cached item asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the item to cache.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="value">The item to cache.</param>
    /// <param name="expiration">The expiration time for the cache entry. If null, a default expiration is used.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous set operation.</returns>
    Task SetAsync<T>(
        string key,
        T value,
        TimeSpan? expiration = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a cached item asynchronously.
    /// </summary>
    /// <param name="key">The cache key.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous remove operation.</returns>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}
