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
            return await this.storeContext.Products.FindAsync(id).ConfigureAwait(false);
        }

        async Task<IReadOnlyCollection<Product>> IProductRepository.GetProducts()
        {
            return await this.storeContext.Products
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
