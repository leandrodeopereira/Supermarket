namespace SupermarketApi.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SupermarketApi.Entities;

    public interface IProductRepository
    {
        Task<Product?> GetProduct(int id);

        Task<IReadOnlyCollection<ProductBrand>> GetProductBrands();

        Task<IReadOnlyCollection<Product>> GetProducts();

        Task<IReadOnlyCollection<ProductType>> GetProductTypes();
    }
}
