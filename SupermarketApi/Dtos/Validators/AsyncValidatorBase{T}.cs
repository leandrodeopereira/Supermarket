namespace SupermarketApi.Dtos.Validators
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation.Validators;

    internal abstract class AsyncValidatorBase<T> : AsyncValidatorBase
    {
        protected AsyncValidatorBase(string errorMessage)
            : base(errorMessage)
        {
        }

        protected sealed override Task<bool> IsValidAsync(PropertyValidatorContext context, CancellationToken cancellationToken)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));

            return context.PropertyValue is T castValue
                ? this.IsValidAsync(castValue, context, cancellationToken)
                : Task.FromResult(true);
        }

        protected abstract Task<bool> IsValidAsync(T value, PropertyValidatorContext context, CancellationToken cancellationToken);
    }
}
