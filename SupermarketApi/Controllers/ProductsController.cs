namespace SupermarketApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using SupermarketApi.Dtos;
    using SupermarketApi.Entities;
    using SupermarketApi.Repositories;
    using SupermarketApi.Specifications;

    [ApiController]
    [Route("api/[controller]")]
    public sealed class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<ProductBrand> productBrandRepository;
        private readonly IRepository<ProductType> productTypeRepository;
        private readonly IMapper mapper;

        public ProductsController(
            IRepository<Product> productRepository,
            IRepository<ProductBrand> productBrandRepository,
            IRepository<ProductType> productTypeRepository,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.productBrandRepository = productBrandRepository;
            this.productTypeRepository = productTypeRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await this.productRepository
                .GetEntityWithSpec(spec)
                .ConfigureAwait(false);

            return product switch
            {
                { } => this.Ok(this.mapper.Map<Product, ProductDto>(product)),
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
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await this.productRepository.GetAsync(spec).ConfigureAwait(false);

            return this.Ok(this.mapper.Map<IReadOnlyCollection<Product>, IReadOnlyCollection<ProductDto>>(products));
        }

        [HttpGet("types")]
        public async Task<ActionResult<ICollection<ProductType>>> GetProductTypes()
        {
            var productTypes = await this.productTypeRepository.GetAllAsync().ConfigureAwait(false);

            return this.Ok(productTypes);
        }
    }
}
