namespace SupermarketApi.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SupermarketApi.Data;
    using SupermarketApi.Entities;

    internal sealed class ProductRepository : IProductRepository
    {
        private readonly StoreContext storeContext;

        public ProductRepository(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }

        async Task<Product?> IProductRepository.GetProduct(int id)
        {
            return await this.storeContext.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .SingleOrDefaultAsync(p => p.Id == id)
                .ConfigureAwait(false);
        }

        async Task<IReadOnlyCollection<ProductBrand>> IProductRepository.GetProductBrands()
        {
            return await this.storeContext.ProductBrands
                .ToListAsync()
                .ConfigureAwait(false);
        }

        async Task<IReadOnlyCollection<Product>> IProductRepository.GetProducts()
        {
            return await this.storeContext.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        async Task<IReadOnlyCollection<ProductType>> IProductRepository.GetProductTypes()
        {
            return await this.storeContext.ProductTypes
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
