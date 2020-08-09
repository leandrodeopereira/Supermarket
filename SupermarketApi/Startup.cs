namespace SupermarketApi
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SupermarketApi.Data.DependencyInjection;
    using SupermarketApi.Mapping.DependecyInjection;
    using SupermarketApi.Profiles;
    using SupermarketApi.Repositories.DependencyInjection;

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
                .UseStatusCodePagesWithReExecute("/errors/{0}")
                .UseHttpsRedirection()
                .UseRouting()
                .UseStaticFiles()
                .UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            _ = serviceCollection
                .AddAutoMapper(typeof(MappingProfiles))
                .AddControllers().Services
                .AddDataInfrastruture(this.Configuration)
                .AddMapping()
                .AddRepositories();
        }
    }
}
