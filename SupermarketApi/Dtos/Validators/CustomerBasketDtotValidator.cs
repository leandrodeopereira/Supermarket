namespace SupermarketApi.Dtos.Validators
{
    using FluentValidation;

    public class CustomerBasketDtotValidator : AbstractValidator<CustomerBasketDto>
    {
        public CustomerBasketDtotValidator()
        {
            _ = this.RuleFor(b => b.Id).NotEmpty();
        }
    }
}
