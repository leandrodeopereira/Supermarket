namespace SupermarketApi.Specifications
{
    using System;
    using System.Linq.Expressions;
    using SupermarketApi.Entities;

    public class ProductsWithTypesAndBrandsSpecification : ASpecWithQueryOperations<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productSpecParams)
        {
            _ = productSpecParams ?? throw new ArgumentNullException(nameof(productSpecParams));

            this.AddInclude(x => x.ProductBrand);
            this.AddInclude(x => x.ProductType);
            this.Expression = x =>
                (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
                (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId);
            this.ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);

            switch (productSpecParams.Sort)
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
