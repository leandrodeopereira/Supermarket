namespace SupermarketApi.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SupermarketApi.Entities;
    using SupermarketApi.Specifications;

    public interface IRepository<T>
        where T : BaseEntity
    {
        Task<int> CountAsync(ASpecWithQueryOperations<T> spec);

        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<IReadOnlyCollection<T>> GetAsync(ASpecWithQueryOperations<T> spec);

        Task<T> GetByIdAsync(int id);

        Task<T> GetEntityWithSpec(ASpecWithQueryOperations<T> spec);
    }
}
