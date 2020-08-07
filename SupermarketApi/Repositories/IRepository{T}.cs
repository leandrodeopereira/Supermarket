namespace SupermarketApi.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SupermarketApi.Entities;

    public interface IRepository<T>
        where T : BaseEntity
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);
    }
}
