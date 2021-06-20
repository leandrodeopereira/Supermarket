namespace SupermarketApi.Configuration.DependencyInjection
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    internal static class ConfigurationRegistrationExtensions
    {
        public static IServiceCollection AddCutomConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            return serviceCollection
                .Configure<ApiSettings>(configuration.GetSection("ApiSettings"))
                .Configure<StripeSettings>(configuration.GetSection("StripeSettings"))
                .Configure<TokenOptions>(configuration.GetSection("Token"));
        }
    }
}
