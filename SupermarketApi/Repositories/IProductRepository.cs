namespace SupermarketApi.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SupermarketApi.Entities;

    public interface IProductRepository
    {
        Task<Product?> GetProduct(int id);

        Task<IReadOnlyCollection<Product>> GetProducts();
    }
}
