namespace SupermarketApi.Extensions
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using StackExchange.Redis;

    public static class RedisRegistrationExtesions
    {
        public static IServiceCollection AddRedisConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            _ = services ?? throw new ArgumentNullException(nameof(services));
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));

            return services
                .AddSingleton<IConnectionMultiplexer>(c =>
                {
                    return ConnectionMultiplexer.Connect(
                        ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"), true));
                });
        }
    }
}
