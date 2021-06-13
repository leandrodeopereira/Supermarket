namespace SupermarketApi.Dtos.Validators
{
    using System;
    using FluentValidation;
    using Microsoft.AspNetCore.Identity;
    using SupermarketApi.Entities.Identity;

    public static class ValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, string> StrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            _ = ruleBuilder ?? throw new ArgumentNullException(nameof(ruleBuilder));

            // HACK: Regex to validate a strong password avaliable on https://regexlib.com/REDetails.aspx?regexp_id=1111
            return ruleBuilder
                .Matches(@"(?=^.{6,10}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$$")
                .WithMessage($"{{PropertyName}} expects at least 1 small-case letter, 1 Capital letter, " +
                    $"1 digit, 1 special character and the length should be between 6-10 characters.");
        }

        public static IRuleBuilderOptions<T, string> DuplicateEmail<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            UserManager<AppUser> userManager)
        {
            _ = ruleBuilder ?? throw new ArgumentNullException(nameof(ruleBuilder));
            _ = userManager ?? throw new ArgumentNullException(nameof(userManager));

            return ruleBuilder.SetAsyncValidator(new DuplicateEmailValidator<T>(userManager));
        }
    }
}
