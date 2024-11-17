using Microsoft.EntityFrameworkCore;
using Identity.Domain.Core.Specifications;
using Identity.Domain.Core.Models;
using System.Linq.Dynamic.Core;

namespace Identity.Infrastructure.Repositories;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = inputQuery;

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.Includes.Aggregate(query,
                                (current, include) => current.Include(include));

        query = specification.IncludeStrings.Aggregate(query,
                                (current, include) => current.Include(include));

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }
        else if (!string.IsNullOrWhiteSpace(specification.OrderByString))
        {
            query = DynamicQueryableExtensions.OrderBy(query, specification.OrderByString);
        }
        else if (!string.IsNullOrWhiteSpace(specification.OrderByDescendingString))
        {
            query = DynamicQueryableExtensions.OrderBy(query, specification.OrderByDescendingString + " DESC");
        }

        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip)
                        .Take(specification.Take);
        }

        return query;
    }
}