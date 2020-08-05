namespace SuperMarketApi.Data.DependencyInjection
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DataRegistrationExtensions
    {
        public static IServiceCollection AddDataInfrastruture(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            return serviceCollection
                .AddDbContext<StoreContext>(x => x.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
