namespace SupermarketApi.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using SupermarketApi.Entities;
    using SupermarketApi.Entities.OrderAggregate;

    [ExcludeFromCodeCoverage]
    public sealed class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext, ILoggerFactory loggerFactory)
        {
            _ = storeContext ?? throw new ArgumentNullException(nameof(storeContext));

            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (!storeContext.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText(path + @"/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<ICollection<ProductBrand>>(brandsData);

                    if (brands is not null)
                    {
                        foreach (var item in brands)
                        {
                            _ = storeContext.ProductBrands.Add(item);
                        }
                    }

                    _ = await storeContext.SaveChangesAsync().ConfigureAwait(false);
                }

                if (!storeContext.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText(path + @"/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<ICollection<ProductType>>(typesData);

                    if (types is not null)
                    {
                        foreach (var item in types)
                        {
                            _ = storeContext.ProductTypes.Add(item);
                        }
                    }

                    _ = await storeContext.SaveChangesAsync().ConfigureAwait(false);
                }

                if (!storeContext.Products.Any())
                {
                    var productsData = File.ReadAllText(path + @"/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<ICollection<Product>>(productsData);

                    if (products is not null)
                    {
                        foreach (var item in products)
                        {
                            _ = storeContext.Products.Add(item);
                        }
                    }

                    _ = await storeContext.SaveChangesAsync().ConfigureAwait(false);
                }

                if (!storeContext.DeliveryMethods.Any())
                {
                    var dmData = File.ReadAllText(path + @"/Data/SeedData/delivery.json");

                    var methods = JsonSerializer.Deserialize<ICollection<DeliveryMethod>>(dmData);

                    if (methods is not null)
                    {
                        foreach (var method in methods)
                        {
                            _ = storeContext.DeliveryMethods.Add(method);
                        }
                    }

                    _ = await storeContext.SaveChangesAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex) when (
                ex is ArgumentException or
                ArgumentNullException or
                DbUpdateConcurrencyException or
                DbUpdateException or
                DirectoryNotFoundException or
                FileNotFoundException or
                IOException or
                JsonException or
                NotSupportedException or
                PathTooLongException or
                UnauthorizedAccessException)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
