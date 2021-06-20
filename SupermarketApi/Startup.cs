namespace SupermarketApi
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using SupermarketApi.Configuration.DependencyInjection;
    using SupermarketApi.Data.DependencyInjection;
    using SupermarketApi.Dtos.Validators.DependencyInjection;
    using SupermarketApi.Extensions;
    using SupermarketApi.Identity.DependencyInjection;
    using SupermarketApi.Mapping.DependecyInjection;
    using SupermarketApi.Middleware;
    using SupermarketApi.Profiles;
    using SupermarketApi.Services.DependencyInjection;

    [ExcludeFromCodeCoverage]
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
                .UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "Content")),
                    RequestPath = "/content"
                })
                .UseClientCorsPolicy()
                .UseAuthentication()
                .UseAuthorization()
                .UseSwaggerDocumentation()
                .UseEndpoints(endpoints =>
                {
                    _ = endpoints.MapControllers();
                    _ = endpoints.MapFallbackToController("Index", "Fallback");
                });
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            _ = serviceCollection
                .AddControllers().Services
                .AddCutomConfiguration(this.Configuration)
                .AddApplicationServices()
                .AddAutoMapper(typeof(MappingProfiles))
                .AddClientCorsPolicy(this.Configuration)
                .AddDataInfrastruture(this.Configuration)
                .AddMvcCore().AddValidators().Services
                .AddIdentityInfrastruture(this.Configuration)
                .AddMapping()
                .AddMediatR(typeof(Startup))
                .AddRedisConfiguration(this.Configuration)
                .AddServices()
                .AddSwaggerDocumentation();
        }
    }
}
