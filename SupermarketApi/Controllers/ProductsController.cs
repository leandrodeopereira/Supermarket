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
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<ProductBrand> productBrandRepository;
        private readonly IRepository<ProductType> productTypeRepository;

        public ProductsController(
            IRepository<Product> productRepository,
            IRepository<ProductBrand> productBrandRepository,
            IRepository<ProductType> productTypeRepository)
        {
            this.productRepository = productRepository;
            this.productBrandRepository = productBrandRepository;
            this.productTypeRepository = productTypeRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await this.productRepository
                .GetByIdAsync(id)
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
            var productBrands = await this.productBrandRepository.GetAllAsync().ConfigureAwait(false);

            return this.Ok(productBrands);
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Product>>> GetProducts()
        {
            var products = await this.productRepository.GetAllAsync().ConfigureAwait(false);

            return this.Ok(products);
        }

        [HttpGet("types")]
        public async Task<ActionResult<ICollection<ProductType>>> GetProductTypes()
        {
            var productTypes = await this.productTypeRepository.GetAllAsync().ConfigureAwait(false);

            return this.Ok(productTypes);
        }
    }
}
