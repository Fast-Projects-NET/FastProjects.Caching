using FastProjects.ResultPattern;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FastProjects.Caching;

/// <summary>
/// Implements caching behavior for queries in the MediatR pipeline.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
/// <param name="cacheService">The cache service used to store and retrieve cached items.</param>
/// <param name="logger">The logger used to log cache hits and misses.</param>
public sealed class QueryCachingBehavior<TRequest, TResponse>(
    ICacheService cacheService,
    ILogger<QueryCachingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
{
    /// <summary>
    /// Handles the caching behavior for the request.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="next">The next delegate in the pipeline.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response.</returns>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse? cachedResult = await cacheService.GetAsync<TResponse>(
            request.CacheKey,
            cancellationToken);

        string name = typeof(TRequest).Name;
        
        if (cachedResult is not null)
        {
            logger.LogInformation("Cache hit for {Query}", name);

            return cachedResult;
        }

        logger.LogInformation("Cache miss for {Query}", name);

        TResponse result = await next();
        
        if ((result is IResult resultPattern && IsSuccess(resultPattern)) ||
            (result is not IResult && result is not null))
        {
            await cacheService.SetAsync(request.CacheKey, result, request.Expiration, cancellationToken);
        }
        
        return result;
    }
    
    private bool IsSuccess(IResult result)=> result.IsOk() || result.IsCreated() || result.IsNoContent();
}
