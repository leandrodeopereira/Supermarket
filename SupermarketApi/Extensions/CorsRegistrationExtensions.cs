namespace SupermarketApi.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class CorsRegistrationExtensions
    {
        private const string CorsPolicy = "CorsPolicy";

        public static IServiceCollection AddClientCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services ?? throw new ArgumentNullException(nameof(services));
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));

            return
                services.AddCors(opt =>
                {
                    opt.AddPolicy(CorsPolicy, policy =>
                    {
                        _ = policy
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins(configuration["ClientUrl"]);
                    });
                });
        }

        public static IApplicationBuilder UseClientCorsPolicy(this IApplicationBuilder applicationBuilder)
        {
            _ = applicationBuilder ?? throw new ArgumentNullException(nameof(applicationBuilder));

            return applicationBuilder.UseCors(CorsPolicy);
        }
    }
}
