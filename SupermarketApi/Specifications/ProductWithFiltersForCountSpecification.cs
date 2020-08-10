namespace SupermarketApi.Specifications
{
    using System;
    using System.Linq.Expressions;
    using SupermarketApi.Entities;

    public class ProductWithFiltersForCountSpecification : ASpecWithQueryOperations<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productSpecParams)
        {
            _ = productSpecParams ?? throw new ArgumentNullException(nameof(productSpecParams));

            this.Expression = x =>
                (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
                (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId);
        }

        public override Expression<Func<Product, bool>> Expression { get; }
    }
}
