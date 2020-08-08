namespace SupermarketApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using SupermarketApi.Dtos;
    using SupermarketApi.Entities;
    using SupermarketApi.Repositories;
    using SupermarketApi.Specifications;

    [TestClass]
    public sealed class ProductsControllerTests
    {
        [TestMethod]
        public async Task GettingProductBrandsShouldReturnExpectedProductBrands()
        {
            // Arrange
            var expectedProductBrands = new List<ProductBrand>
            {
                new ProductBrand { Id = 1, Name = "Product Brand 1" },
                new ProductBrand { Id = 2, Name = "Product Brand 2" },
            };

            var productBrandRepository = Substitute.For<IRepository<ProductBrand>>();
            _ = productBrandRepository
                .GetAllAsync()
                .Returns(expectedProductBrands);

            var productController = CreateProductsController(productBrandRepository: productBrandRepository);

            // Act
            var productBrandsActionResult = await productController.GetProductBrands().ConfigureAwait(false);

            // Assert
            productBrandsActionResult.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(expectedProductBrands);
        }

        [TestMethod]
        public async Task GettingProductsShouldReturnExpectedProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" },
            };

            var expectedProductsDto = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Product 1" },
                new ProductDto { Id = 2, Name = "Product 2" },
            };

            var productRepository = Substitute.For<IRepository<Product>>();
            _ = productRepository
                .GetAsync(Arg.Any<ASpecWithInclude<Product>>())
                .Returns(products);

            var mapper = Substitute.For<IMapper>();
            _ = mapper
                .Map<IReadOnlyCollection<Product>, IReadOnlyCollection<ProductDto>>(products)
                .Returns(expectedProductsDto);

            var productController = CreateProductsController(
                productRepository: productRepository,
                mapper: mapper);

            // Act
            var productsActionResult = await productController.GetProducts().ConfigureAwait(false);

            // Assert
            productsActionResult.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(expectedProductsDto);
        }

        [TestMethod]
        public async Task GettingProductTypesShouldReturnExpectedProductTypes()
        {
            // Arrange
            var expectedProductTypes = new List<ProductType>
            {
                new ProductType { Id = 1, Name = "Product Type 1" },
                new ProductType { Id = 2, Name = "Product Type 2" },
            };

            var productTypeRepository = Substitute.For<IRepository<ProductType>>();
            _ = productTypeRepository
                .GetAllAsync()
                .Returns(expectedProductTypes);

            var productController = CreateProductsController(productTypeRepository: productTypeRepository);

            // Act
            var productTypesActionResult = await productController.GetProductTypes().ConfigureAwait(false);

            // Assert
            productTypesActionResult.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(expectedProductTypes);
        }

        [TestMethod]
        public async Task GettingProductByIdShouldReturnExpectedProduct()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" },
            };

            var expectedProductDto = new ProductDto { Id = 1, Name = "Product 1" };

            var productRepository = Substitute.For<IRepository<Product>>();
            _ = productRepository
                .GetEntityWithSpec(Arg.Any<ASpecWithInclude<Product>>())
                .Returns(products[0]);

            var mapper = Substitute.For<IMapper>();
            _ = mapper
                .Map<Product, ProductDto>(products[0])
                .Returns(expectedProductDto);

            var productController = CreateProductsController(
                productRepository: productRepository,
                mapper: mapper);

            // Act
            var productActionResult = await productController.GetProduct(1).ConfigureAwait(false);

            // Assert
            _ = productActionResult.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeSameAs(expectedProductDto);
        }

        [TestMethod]
        public async Task GettingProductByIdGivenIdDoesnotExistShouldReturnNotFound()
        {
            // Arrange
            var productController = CreateProductsController(productRepository: Substitute.For<IRepository<Product>>());

            // Act
            var notFoundActionResult = await productController.GetProduct(1).ConfigureAwait(false);

            // Assert
            _ = notFoundActionResult.Result.Should().BeOfType<NotFoundResult>();
        }

        private static ProductsController CreateProductsController(
            IRepository<Product> productRepository = default!,
            IRepository<ProductBrand> productBrandRepository = default!,
            IRepository<ProductType> productTypeRepository = default!,
            IMapper mapper = default!)
        {
            return new ProductsController(
                productRepository ?? Substitute.For<IRepository<Product>>(),
                productBrandRepository ?? Substitute.For<IRepository<ProductBrand>>(),
                productTypeRepository ?? Substitute.For<IRepository<ProductType>>(),
                mapper ?? Substitute.For<IMapper>());
        }
    }
}
