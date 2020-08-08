namespace SupermarketApi.Repositories.DependencyInjection
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using SupermarketApi.Repositories;

    public static class RepositoryRegistrationExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            _ = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));

            return serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        }
    }
}
