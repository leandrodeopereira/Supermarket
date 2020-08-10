namespace SupermarketApi.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using NSpecifications;

    public abstract class ASpecWithQueryOperations<T> : ASpec<T>
    {
        public ICollection<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public bool IsPagingEnabled { get; private set; } = false;

        public Expression<Func<T, object>>? OrderBy { get; set; }

        public Expression<Func<T, object>>? OrderByDescending { get; set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            this.Includes.Add(includeExpression);
        }

        protected void ApplyPaging(int skip, int take)
        {
            this.Skip = skip;
            this.Take = take;
            this.IsPagingEnabled = true;
        }
    }
}
