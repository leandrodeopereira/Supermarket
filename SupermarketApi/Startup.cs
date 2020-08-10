namespace SupermarketApi
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SupermarketApi.Data.DependencyInjection;
    using SupermarketApi.Extensions;
    using SupermarketApi.Mapping.DependecyInjection;
    using SupermarketApi.Middleware;
    using SupermarketApi.Profiles;

    [ExcludeFromCodeCoverage]
    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Instantiated through reflection")]
    internal sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static void Configure(IApplicationBuilder applicationBuilder)
        {
            _ = applicationBuilder
                .UseMiddleware<ExceptionMiddleware>()
                .UseStatusCodePagesWithReExecute("/errors/{0}")
                .UseHttpsRedirection()
                .UseRouting()
                .UseStaticFiles()
                .UseClientCorsPolicy()
                .UseSwaggerDocumentation()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            _ = serviceCollection
                .AddControllers().Services
                .AddApplicationServices()
                .AddAutoMapper(typeof(MappingProfiles))
                .AddClientCorsPolicy(this.Configuration)
                .AddDataInfrastruture(this.Configuration)
                .AddMapping()
                .AddSwaggerDocumentation();
        }
    }
}
