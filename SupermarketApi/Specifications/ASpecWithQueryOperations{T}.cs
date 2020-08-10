namespace SupermarketApi.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using NSpecifications;

    public abstract class ASpecWithQueryOperations<T> : ASpec<T>
    {
        public ICollection<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            this.Includes.Add(includeExpression);
        }
    }
}
