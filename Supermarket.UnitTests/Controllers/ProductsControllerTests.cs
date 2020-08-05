namespace SupermarketApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using SupermarketApi.Entities;
    using SupermarketApi.Repositories;

    [TestClass]
    public sealed class ProductsControllerTests
    {
        [TestMethod]
        public async Task GettingProductsShouldReturnExpectedProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product(1, "Product 1"),
                new Product(2, "Product 2"),
            };

            var productRepository = Substitute.For<IProductRepository>();
            _ = productRepository
                .GetProducts()
                .Returns(expectedProducts);

            var productController = new ProductsController(productRepository);

            // Act
            var productsActionResult = await productController.GetProducts().ConfigureAwait(false);

            // Assert
            productsActionResult.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(expectedProducts);
        }

        [TestMethod]
        public async Task GettingProductByIdShouldReturnExpectedProduct()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product(1, "Product 1"),
                new Product(2, "Product 2"),
            };

            var productRepository = Substitute.For<IProductRepository>();
            _ = productRepository
                .GetProduct(1)
                .Returns(expectedProducts[0]);

            var productController = new ProductsController(productRepository);

            // Act
            var productActionResult = await productController.GetProduct(1).ConfigureAwait(false);

            // Assert
            _ = productActionResult.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeSameAs(expectedProducts[0]);
        }

        [TestMethod]
        public async Task GettingProductByIdGivenIdDoesnotExistShouldReturnNotFound()
        {
            // Arrange
            var productController = new ProductsController(Substitute.For<IProductRepository>());

            // Act
            var notFoundActionResult = await productController.GetProduct(1).ConfigureAwait(false);

            // Assert
            _ = notFoundActionResult.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
