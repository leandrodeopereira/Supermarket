namespace SupermarketApi.Specifications
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using SupermarketApi.Entities;

    [SuppressMessage("Globalization", "CA1304: Specify CultureInfo", Justification = "StringComparison is not supported by EF")]
    [SuppressMessage("Globalization", "CA1307: Specify StringComparison", Justification = "StringComparison is not supported by EF")]
    public class ProductWithFiltersForCountSpecification : ASpecWithQueryOperations<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productSpecParams)
        {
            _ = productSpecParams ?? throw new ArgumentNullException(nameof(productSpecParams));

            this.Expression = x =>
                (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search)) &&
                (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
                (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId);
        }

        public override Expression<Func<Product, bool>> Expression { get; }
    }
}
