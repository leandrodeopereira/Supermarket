namespace SupermarketApi.Dtos.Validators.DependencyInjection
{
    using System;
    using FluentValidation.AspNetCore;
    using Microsoft.Extensions.DependencyInjection;

    internal static class ValidatorsRegistrationExtensions
    {
        public static IMvcCoreBuilder AddValidators(this IMvcCoreBuilder mvcCoreBuilder)
        {
            _ = mvcCoreBuilder ?? throw new ArgumentNullException(nameof(mvcCoreBuilder));

            return mvcCoreBuilder
                .AddFluentValidation(fv =>
                {
                    _ = fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    fv.ImplicitlyValidateChildProperties = true;
                });
        }
    }
}
