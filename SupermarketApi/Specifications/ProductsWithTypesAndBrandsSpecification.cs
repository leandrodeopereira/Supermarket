namespace SupermarketApi.Specifications
{
    using System;
    using System.Linq.Expressions;
    using SupermarketApi.Entities;

    public class ProductsWithTypesAndBrandsSpecification : ASpecWithQueryOperations<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(string? sort)
        {
            this.AddInclude(x => x.ProductBrand);
            this.AddInclude(x => x.ProductType);
            this.Expression = default!;

            switch (sort)
            {
                case "priceAsc":
                    this.OrderBy = p => p.Price;
                    break;

                case "priceDesc":
                    this.OrderByDescending = p => p.Price;
                    break;

                default:
                    this.OrderBy = p => p.Name;
                    break;
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id)
        {
            this.Expression = x => x.Id == id;
            this.AddInclude(x => x.ProductBrand);
            this.AddInclude(x => x.ProductType);
        }

        public override Expression<Func<Product, bool>> Expression { get; }
    }
}
