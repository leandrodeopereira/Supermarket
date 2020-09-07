namespace SupermarketApi.Services
{
    using System;
    using System.Threading.Tasks;
    using SupermarketApi.Entities;
    using SupermarketApi.Repositories;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>()
            where TEntity : BaseEntity;

        Task<int> Complete();
    }
}
