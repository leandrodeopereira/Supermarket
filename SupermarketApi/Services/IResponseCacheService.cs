namespace SupermarketApi.Services
{
    using System;
    using System.Threading.Tasks;

    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);

        Task<string?> GetCachedResponseAsync(string cacheKey);
    }
}
