namespace SupermarketApi.Identity.DependencyInjection
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class IdentityInfrastrutureRegistrationExtensions
    {
        public static IServiceCollection AddIdentityInfrastruture(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            return serviceCollection
                .AddDbContext<AppIdentityDbContext>(x => x.UseSqlite(configuration.GetConnectionString("IdentityConnection")));
        }
    }
}
