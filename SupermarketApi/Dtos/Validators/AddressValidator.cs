namespace SupermarketApi.Dtos.Validators
{
    using FluentValidation;

    public class AddressValidator : AbstractValidator<AddressDto>
    {
        public AddressValidator()
        {
            _ = this.RuleFor(o => o.City).NotEmpty();
            _ = this.RuleFor(o => o.Country).NotEmpty();
            _ = this.RuleFor(o => o.FirstName).NotEmpty();
            _ = this.RuleFor(o => o.LastName).NotEmpty();
            _ = this.RuleFor(o => o.State).NotEmpty();
            _ = this.RuleFor(o => o.Street).NotEmpty();
            _ = this.RuleFor(o => o.ZipCode).NotEmpty();
        }
    }
}
