namespace SupermarketApi.Services
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using StackExchange.Redis;

    public sealed class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase database;

        public ResponseCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            this.database = connectionMultiplexer.GetDatabase();
        }

        async Task IResponseCacheService.CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response is null)
            {
                return;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serialisedResponse = JsonSerializer.Serialize(response, options);

            _ = await this.database.StringSetAsync(cacheKey, serialisedResponse, timeToLive);
        }

        async Task<string?> IResponseCacheService.GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await this.database.StringGetAsync(cacheKey);

            return cachedResponse.IsNullOrEmpty ? default : (string)cachedResponse;
        }
    }
}
