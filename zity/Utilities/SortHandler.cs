using Microsoft.EntityFrameworkCore;

namespace zity.Utilities
{
    public class SortHandler<T>
    {
        public IQueryable<T> ApplySorting(IQueryable<T> query, string sortExpression)
        {
            if (string.IsNullOrEmpty(sortExpression)) return query;

            var sortExpressions = sortExpression.Split(',');
            foreach (var expression in sortExpressions)
            {
                var trimmedExpression = expression.Trim();
                if (string.IsNullOrEmpty(trimmedExpression)) continue;

                bool isDescending = trimmedExpression.StartsWith("-");
                var propertyName = isDescending ? trimmedExpression.Substring(1) : trimmedExpression;

                propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1);

                query = isDescending
                    ? query.OrderByDescending(e => EF.Property<object>(e, propertyName))
                    : query.OrderBy(e => EF.Property<object>(e, propertyName));
            }

            return query;
        }
    }

}