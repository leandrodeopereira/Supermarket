namespace SupermarketApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using SupermarketApi.Entities;
    using SupermarketApi.Repositories;

    [ApiController]
    [Route("api/[controller]")]
    public sealed class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await this.productRepository
                .GetProduct(id)
                .ConfigureAwait(false);

            return product switch
            {
                { } => this.Ok(product),
                _ => this.NotFound(),
            };
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ICollection<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await this.productRepository.GetProductBrands().ConfigureAwait(false);

            return this.Ok(productBrands);
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Product>>> GetProducts()
        {
            var products = await this.productRepository.GetProducts().ConfigureAwait(false);

            return this.Ok(products);
        }

        [HttpGet("types")]
        public async Task<ActionResult<ICollection<ProductType>>> GetProductTypes()
        {
            var productTypes = await this.productRepository.GetProductTypes().ConfigureAwait(false);

            return this.Ok(productTypes);
        }
    }
}
