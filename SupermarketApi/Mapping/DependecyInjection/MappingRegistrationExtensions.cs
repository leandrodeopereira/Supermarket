namespace SupermarketApi.Mapping.DependecyInjection
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Extensions.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public static class MappingRegistrationExtensions
    {
        public static IServiceCollection AddMapping(this IServiceCollection serviceCollection)
        {
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            return serviceCollection
                .Scan(scan => scan
                    .FromAssembliesOf(typeof(IBuilder<,>))
                    .AddClasses(classes => classes.AssignableToAny(
                        typeof(IBuilder<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
        }
    }
}
