namespace SupermarketApi.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SupermarketApi.Data;
    using SupermarketApi.Entities;

    [TestClass]
    public sealed class BaseRepositoryTests
    {
        [TestMethod]
        public async Task GettingProductByIdShouldReturnExpectedProduct()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var expectedProduct = new Product
            {
                Id = 1,
                Name = "Product 1",
            };

            using (var context = new StoreContext(options))
            {
                _ = context.Products.Add(expectedProduct);
                _ = context.SaveChanges();
            }

            using (var context = new StoreContext(options))
            {
                IRepository<Product> productRepository = new BaseRepository<Product>(context);

                // Act
                var product = await productRepository.GetByIdAsync(1).ConfigureAwait(false);

                // Assert
                product.Should().BeEquivalentTo(expectedProduct);
            }
        }

        [TestMethod]
        public async Task GettingProductsShouldReturnExpectedProducts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" },
            };

            using (var context = new StoreContext(options))
            {
                context.Products.AddRange(expectedProducts);
                _ = context.SaveChanges();
            }

            using (var context = new StoreContext(options))
            {
                IRepository<Product> productRepository = new BaseRepository<Product>(context);

                // Act
                var products = await productRepository.GetAllAsync().ConfigureAwait(false);

                // Assert
                _ = products.Should().BeEquivalentTo(expectedProducts);
            }
        }
    }
}
