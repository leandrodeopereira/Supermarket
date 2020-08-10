namespace SupermarketApi.Repositories
{
    using System;
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

        Task<int> IRepository<T>.CountAsync(ASpecWithQueryOperations<T> spec)
        {
            _ = spec ?? throw new ArgumentNullException(nameof(spec));

            return CreateTask();

            async Task<int> CreateTask()
            {
                return await this.storeContext.ApplySpecification(spec).CountAsync().ConfigureAwait(false);
            }
        }

        async Task<IReadOnlyCollection<T>> IRepository<T>.GetAllAsync()
        {
            return await this.storeContext.Set<T>()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        async Task<IReadOnlyCollection<T>> IRepository<T>.GetAsync(ASpecWithQueryOperations<T> spec)
        {
            return await this.storeContext.ApplySpecification(spec).ToListAsync().ConfigureAwait(false);
        }

        async Task<T> IRepository<T>.GetByIdAsync(int id)
        {
            return await this.storeContext.Set<T>()
                .FindAsync(id)
                .ConfigureAwait(false);
        }

        async Task<T> IRepository<T>.GetEntityWithSpec(ASpecWithQueryOperations<T> spec)
        {
            return await this.storeContext.ApplySpecification(spec).FirstOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
