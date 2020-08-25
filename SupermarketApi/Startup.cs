namespace SupermarketApi
{
    using System.Diagnostics.CodeAnalysis;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SupermarketApi.Data.DependencyInjection;
    using SupermarketApi.Dtos.Validators.DependencyInjection;
    using SupermarketApi.Extensions;
    using SupermarketApi.Identity.DependencyInjection;
    using SupermarketApi.Mapping.DependecyInjection;
    using SupermarketApi.Middleware;
    using SupermarketApi.Profiles;
    using SupermarketApi.Services.DependencyInjection;

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
                .UseAuthentication()
                .UseAuthorization()
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
                .AddMvcCore().AddValidators().Services
                .AddIdentityInfrastruture(this.Configuration)
                .AddMapping()
                .AddRedisConfiguration(this.Configuration)
                .AddServices()
                .AddSwaggerDocumentation();
        }
    }
}
