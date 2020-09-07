namespace SupermarketApi.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SupermarketApi.Data;
    using SupermarketApi.Entities;
    using SupermarketApi.Specifications;
    using static Microsoft.EntityFrameworkCore.EntityState;

    internal class BaseRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly StoreContext storeContext;

        public BaseRepository(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        void IRepository<T>.Add(T entity)
        {
            _ = this.storeContext.Set<T>().Add(entity);
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

        void IRepository<T>.Delete(T entity)
        {
            _ = this.storeContext.Set<T>().Remove(entity);
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

        void IRepository<T>.Update(T entity)
        {
            _ = this.storeContext.Set<T>().Attach(entity);
            _ = this.storeContext.Entry(entity).State = Modified;
        }
    }
}
