#nullable disable
namespace SupermarketApi.Dtos.Validators
{
    using FluentValidation;

    public class BasketItemDtotValidator : AbstractValidator<BasketItemDto>
    {
        public BasketItemDtotValidator()
        {
            _ = this.RuleFor(b => b.Brand).NotEmpty();
            _ = this.RuleFor(b => b.Id).NotEmpty();
            _ = this.RuleFor(b => b.PictureUrl).NotEmpty();
            _ = this.RuleFor(b => b.Price).GreaterThan(0);
            _ = this.RuleFor(b => b.ProductName).NotEmpty();
            _ = this.RuleFor(b => b.ProductType).NotEmpty();
            _ = this.RuleFor(b => b.Quantity).GreaterThan(0);
        }
    }
}
