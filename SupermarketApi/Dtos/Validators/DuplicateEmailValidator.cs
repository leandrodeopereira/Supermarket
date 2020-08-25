namespace SupermarketApi.Dtos.Validators
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation.Validators;
    using Microsoft.AspNetCore.Identity;
    using SupermarketApi.Entities.Identity;

    internal class DuplicateEmailValidator : AsyncValidatorBase<string>
    {
        private readonly UserManager<AppUser> userManager;

        public DuplicateEmailValidator(UserManager<AppUser> userManager)
            : base("{PropertyName} '{PropertyValue}' already registred.")
        {
            this.userManager = userManager;
        }

        protected override async Task<bool> IsValidAsync(string email, PropertyValidatorContext context, CancellationToken cancellationToken)
        {
            _ = email ?? throw new ArgumentNullException(nameof(email));

            return await this.userManager.FindByEmailAsync(email).ConfigureAwait(false) == null;
        }
    }
}
