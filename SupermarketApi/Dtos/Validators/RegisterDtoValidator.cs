namespace SupermarketApi.Dtos.Validators
{
    using FluentValidation;
    using Microsoft.AspNetCore.Identity;
    using SupermarketApi.Entities.Identity;

    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator(UserManager<AppUser> userManager)
        {
            _ = this.RuleFor(a => a.Email).EmailAddress().DuplicateEmail(userManager);
            _ = this.RuleFor(a => a.Password).StrongPassword();
        }
    }
}
