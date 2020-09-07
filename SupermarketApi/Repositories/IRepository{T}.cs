namespace SupermarketApi.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SupermarketApi.Entities;
    using SupermarketApi.Specifications;

    public interface IRepository<T>
        where T : BaseEntity
    {
        void Add(T entity);

        Task<int> CountAsync(ASpecWithQueryOperations<T> spec);

        void Delete(T entity);

        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<IReadOnlyCollection<T>> GetAsync(ASpecWithQueryOperations<T> spec);

        Task<T> GetByIdAsync(int id);

        Task<T> GetEntityWithSpec(ASpecWithQueryOperations<T> spec);

        void Update(T entity);
    }
}
