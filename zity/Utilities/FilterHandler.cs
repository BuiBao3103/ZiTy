using Microsoft.EntityFrameworkCore;
using zity.ExceptionHandling;

namespace ZiTy.Utilities
{
    public class FilterHandler<T>
    {
        public IQueryable<T> ApplyFilters(IQueryable<T> query, Dictionary<string, string> filters)
        {
            foreach (var filter in filters)
            {
                if (string.IsNullOrEmpty(filter.Value)) continue;

                var filterParts = filter.Value.Split(':');
                if (filterParts.Length != 2)
                {
                    throw new AppError($"Invalid filter format for {filter.Key}");
                }

                var operatorType = filterParts[0];
                var value = filterParts[1];

                query = ApplyFilter(query, filter.Key, operatorType, value);
            }

            return query;
        }

        private IQueryable<T> ApplyFilter(IQueryable<T> query, string property, string operatorType, string value)
        {
            switch (operatorType)
            {
                case "eq":
                    query = query.Where(e => EF.Property<object>(e, property).ToString() == value);
                    break;
                case "neq":
                    query = query.Where(e => EF.Property<object>(e, property).ToString() != value);
                    break;
                case "gt":
                    query = query.Where(e => Convert.ToDouble(EF.Property<object>(e, property)) > Convert.ToDouble(value));
                    break;
                case "gte":
                    query = query.Where(e => Convert.ToDouble(EF.Property<object>(e, property)) >= Convert.ToDouble(value));
                    break;
                case "lt":
                    query = query.Where(e => Convert.ToDouble(EF.Property<object>(e, property)) < Convert.ToDouble(value));
                    break;
                case "lte":
                    query = query.Where(e => Convert.ToDouble(EF.Property<object>(e, property)) <= Convert.ToDouble(value));
                    break;
                case "like":
                    query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, property), $"%{value}%"));
                    break;
                case "in":
                    var values = value.Split(',').ToList();
                    query = query.Where(e => values.Contains(EF.Property<string>(e, property)));
                    break;
                default:
                    throw new AppError($"Invalid filter operator: {operatorType}");
            }

            return query;
        }
    }
}
