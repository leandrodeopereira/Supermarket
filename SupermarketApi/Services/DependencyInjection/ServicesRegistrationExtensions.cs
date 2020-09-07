namespace SupermarketApi.Services.DependencyInjection
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    internal static class ServicesRegistrationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            return serviceCollection
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
