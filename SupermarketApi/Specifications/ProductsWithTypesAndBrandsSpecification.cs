namespace SupermarketApi.Specifications
{
    using System;
    using System.Linq.Expressions;
    using SupermarketApi.Entities;

    public class ProductsWithTypesAndBrandsSpecification : ASpecWithQueryOperations<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            this.AddInclude(x => x.ProductBrand);
            this.AddInclude(x => x.ProductType);
            this.Expression = default!;
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
