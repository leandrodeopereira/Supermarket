namespace SupermarketApi.Services
{
    using System;
    using System.Collections;
    using System.Threading.Tasks;
    using SupermarketApi.Data;
    using SupermarketApi.Repositories;

    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext storeContext;
        private readonly Hashtable repositories;

        public UnitOfWork(StoreContext storeContext)
        {
            this.storeContext = storeContext;
            this.repositories = new Hashtable();
        }

        async Task<int> IUnitOfWork.Complete()
        {
            return await this.storeContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.storeContext.Dispose();
        }

        IRepository<TEntity> IUnitOfWork.Repository<TEntity>()
        {
            var type = typeof(TEntity).Name;

            if (!this.repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositoryType.MakeGenericType(typeof(TEntity)),
                    this.storeContext);

                this.repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)this.repositories[type]!;
        }
    }
}
