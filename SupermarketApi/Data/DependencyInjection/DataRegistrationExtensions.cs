namespace SupermarketApi.Data.DependencyInjection
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class DataRegistrationExtensions
    {
        public static IServiceCollection AddDataInfrastruture(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            return serviceCollection
                .AddDbContext<StoreContext>(x => x.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
