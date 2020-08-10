namespace SupermarketApi.Data
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using SupermarketApi.Entities;
    using SupermarketApi.Specifications;

    public static class DbContextExtensions
    {
        public static IQueryable<T> ApplySpecification<T>(this DbContext context, ASpecWithQueryOperations<T> specification)
            where T : BaseEntity
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            _ = specification ?? throw new ArgumentNullException(nameof(specification));

            return ASpecWithQueryOperationsEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), specification);
        }
    }
}
