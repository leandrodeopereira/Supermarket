namespace SupermarketApi.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using NSpecifications;

    public abstract class ASpecWithQueryOperations<T> : ASpec<T>
    {
        public ICollection<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>>? OrderBy { get; set; }

        public Expression<Func<T, object>>? OrderByDescending { get; set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            this.Includes.Add(includeExpression);
        }
    }
}
