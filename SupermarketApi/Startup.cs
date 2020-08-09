namespace SupermarketApi
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SupermarketApi.Data.DependencyInjection;
    using SupermarketApi.Errors;
    using SupermarketApi.Mapping.DependecyInjection;
    using SupermarketApi.Middleware;
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
                .UseMiddleware<ExceptionMiddleware>()
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
                .Configure<ApiBehaviorOptions>(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var errors = actionContext.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .SelectMany(x => x.Value.Errors)
                            .Select(x => x.ErrorMessage).ToArray();

                        return new BadRequestObjectResult(new ApiValidationErrorResponse(errors));
                    };
                })
                .AddDataInfrastruture(this.Configuration)
                .AddMapping()
                .AddRepositories();
        }
    }
}
