namespace SupermarketApi.Dtos.Validators
{
    using FluentValidation;

    public class OrderValidator : AbstractValidator<OrderDto>
    {
        public OrderValidator()
        {
            _ = this.RuleFor(o => o.BasketId).NotEmpty();
            _ = this.RuleFor(o => o.DeliveryMethodId).NotEmpty();
            _ = this.RuleFor(o => o.ShipToAddress).NotNull();
        }
    }
}
