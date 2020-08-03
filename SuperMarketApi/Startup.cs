namespace SuperMarketApi
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
                .UseHttpsRedirection()
                .UseRouting()
                .UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
            });
        }

        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            _ = serviceCollection.AddControllers();
        }
    }
}
