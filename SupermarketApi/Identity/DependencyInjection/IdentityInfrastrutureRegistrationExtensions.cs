namespace SupermarketApi.Identity.DependencyInjection
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SupermarketApi.Entities.Identity;

    [ExcludeFromCodeCoverage]
    public static class IdentityInfrastrutureRegistrationExtensions
    {
        public static IServiceCollection AddIdentityInfrastruture(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            var builder = serviceCollection.AddIdentityCore<AppUser>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);
            _ = builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            _ = builder.AddSignInManager<SignInManager<AppUser>>();

            return serviceCollection
                .AddDbContext<AppIdentityDbContext>(x => x.UseSqlite(configuration.GetConnectionString("IdentityConnection")))
                .AddAuthentication().Services;
        }
    }
}
