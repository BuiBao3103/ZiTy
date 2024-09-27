using Microsoft.EntityFrameworkCore;
using zity.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;

namespace zity.Utilities
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

                query = FilterHandler<T>.ApplyFilter(query, filter.Key, operatorType, value);
            }

            return query;
        }



        private static IQueryable<T> ApplyFilter(IQueryable<T> query, string property, string operatorType, string value)
        {
            switch (operatorType)
            {
                case "eq":
                    query = query.Where(e => e != null && EF.Property<string>(e, property) == value);
                    break;
                case "neq":
                    query = query.Where(e => e != null && EF.Property<string>(e, property) != value);
                    break;
                case "gt":
                    query = query.Where(e => e != null && EF.Property<object>(e, property) != null && Convert.ToDouble(EF.Property<object>(e, property)) > Convert.ToDouble(value));
                    break;
                case "gte":
                    query = query.Where(e => e != null && EF.Property<object>(e, property) != null && Convert.ToDouble(EF.Property<object>(e, property)) >= Convert.ToDouble(value));
                    break;
                case "lt":
                    query = query.Where(e => e != null && EF.Property<object>(e, property) != null && Convert.ToDouble(EF.Property<object>(e, property)) < Convert.ToDouble(value));
                    break;
                case "lte":
                    query = query.Where(e => e != null && EF.Property<object>(e, property) != null && Convert.ToDouble(EF.Property<object>(e, property)) <= Convert.ToDouble(value));
                    break;
                case "like":
                    query = query.Where(e => e != null && EF.Property<string>(e, property) != null && EF.Functions.Like(EF.Property<string>(e, property), $"%{value}%"));
                    break;
                case "in":
                    var values = value.Split(',').ToList();
                    query = query.Where(e => e != null && EF.Property<string>(e, property) != null && values.Contains(EF.Property<string>(e, property)));
                    break;
                default:
                    throw new AppError($"Invalid filter operator: {operatorType}");
            }

            return query;
        }
    }
}
