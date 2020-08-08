namespace SupermarketApi.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SupermarketApi.Data;
    using SupermarketApi.Entities;
    using SupermarketApi.Specifications;

    internal class BaseRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly StoreContext storeContext;

        public BaseRepository(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        async Task<IReadOnlyCollection<T>> IRepository<T>.GetAllAsync()
        {
            return await this.storeContext.Set<T>()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        async Task<IReadOnlyCollection<T>> IRepository<T>.GetAsync(ASpecWithInclude<T> spec)
        {
            return await this.storeContext.ApplySpecification(spec).ToListAsync().ConfigureAwait(false);
        }

        async Task<T> IRepository<T>.GetByIdAsync(int id)
        {
            return await this.storeContext.Set<T>()
                .FindAsync(id)
                .ConfigureAwait(false);
        }

        async Task<T> IRepository<T>.GetEntityWithSpec(ASpecWithInclude<T> spec)
        {
            return await this.storeContext.ApplySpecification(spec).FirstOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
