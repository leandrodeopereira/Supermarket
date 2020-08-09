namespace SupermarketApi.Extensions
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using SupermarketApi.Errors;
    using SupermarketApi.Repositories.DependencyInjection;

    public static class ApplicationServiceRegistrationExtesions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            _ = services ?? throw new ArgumentNullException(nameof(services));

            return services
                .AddRepositories()
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
                });
        }
    }
}
