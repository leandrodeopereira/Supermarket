namespace SupermarketApi.Identity.DependencyInjection
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
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
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidIssuer = configuration["Token:Issuer"],
                        };
                    })
                .Services;
        }
    }
}
