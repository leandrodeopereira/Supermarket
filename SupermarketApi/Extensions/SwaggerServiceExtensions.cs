namespace SupermarketApi.Extensions
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            _ = services ?? throw new ArgumentNullException(nameof(services));

            return services
                .AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Supermarket API", Version = "v1" }));
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder applicationBuilder)
        {
            _ = applicationBuilder ?? throw new ArgumentNullException(nameof(applicationBuilder));

            return applicationBuilder
                .UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Supermarket API v1"));
        }
    }
}
