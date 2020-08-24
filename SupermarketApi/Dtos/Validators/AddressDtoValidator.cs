namespace SupermarketApi.Dtos.Validators
{
    using FluentValidation;

    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            _ = this.RuleFor(a => a.City).NotEmpty();
            _ = this.RuleFor(a => a.FirstName).NotEmpty();
            _ = this.RuleFor(a => a.LastName).NotEmpty();
            _ = this.RuleFor(a => a.State).NotEmpty();
            _ = this.RuleFor(a => a.Street).NotEmpty();
            _ = this.RuleFor(a => a.ZipCode).NotEmpty();
        }
    }
}
