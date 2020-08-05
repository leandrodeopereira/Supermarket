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
    public class ProductRepositoryTests
    {
        [TestMethod]
        public async Task GettingProductByIdShouldReturnExpectedProduct()
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var expectedProduct = new Product(1, "Product 1");

            using (var context = new StoreContext(options))
            {
                _ = context.Products.Add(expectedProduct);
                _ = context.SaveChanges();
            }

            using (var context = new StoreContext(options))
            {
                IProductRepository productRepository = new ProductRepository(context);

                // Act
                var product = await productRepository.GetProduct(1).ConfigureAwait(false);

                // Assert
                product.Should().BeEquivalentTo(expectedProduct);
            }
        }

        [TestMethod]
        public async Task GettingProductsShouldReturnExpectedProducts()
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var expectedProducts = new List<Product>
            {
                new Product(1, "Product 1"),
                new Product(2, "Product 2"),
            };

            using (var context = new StoreContext(options))
            {
                context.Products.AddRange(expectedProducts);
                _ = context.SaveChanges();
            }

            using (var context = new StoreContext(options))
            {
                IProductRepository productRepository = new ProductRepository(context);

                // Act
                var products = await productRepository.GetProducts().ConfigureAwait(false);

                // Assert
                _ = products.Should().BeEquivalentTo(expectedProducts);
            }
        }
    }
}
