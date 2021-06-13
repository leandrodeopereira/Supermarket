namespace SupermarketApi.Dtos.Validators
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using FluentValidation.Validators;
    using Microsoft.AspNetCore.Identity;
    using SupermarketApi.Entities.Identity;

    internal class DuplicateEmailValidator<T> : AsyncPropertyValidator<T, string>
    {
        private readonly UserManager<AppUser> userManager;

        public DuplicateEmailValidator(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public override string Name => "DuplicateEmailValidator";

        public override async Task<bool> IsValidAsync(ValidationContext<T> context, string email, CancellationToken cancellation)
        {
            _ = email ?? throw new ArgumentNullException(nameof(email));

            return await this.userManager.FindByEmailAsync(email).ConfigureAwait(false) == null;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "{PropertyName} '{PropertyValue}' already registred.";
        }
    }
}
