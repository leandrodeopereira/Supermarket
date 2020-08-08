namespace SupermarketApi.Data
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using SupermarketApi.Entities;
    using SupermarketApi.Specifications;

    internal static class ASpecWithIncludeEvaluator<TEntity>
        where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(
            IQueryable<TEntity> inputQuery,
            ASpecWithInclude<TEntity> spec)
        {
            _ = inputQuery ?? throw new System.ArgumentNullException(nameof(inputQuery));
            _ = spec ?? throw new System.ArgumentNullException(nameof(spec));

            var query = inputQuery;

            if (spec.Expression != null)
            {
                query = query.Where(spec.Expression);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
