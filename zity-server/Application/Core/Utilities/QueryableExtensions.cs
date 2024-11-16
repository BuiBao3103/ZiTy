//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using Microsoft.EntityFrameworkCore;
//using zity.ExceptionHandling;
//using zity.ExceptionHandling.Exceptions;

//namespace zity.Utilities
//{
//    public static class QueryableExtensions
//    {
//        public static IQueryable<T> ApplyIncludes<T>(this IQueryable<T> query, string? includes) where T : class
//        {
//            if (string.IsNullOrEmpty(includes)) return query;
//            string[] includesArray = includes.Split(',');

//            foreach (var include in includesArray)
//            {
//                var trimmedInclude = include.Trim();
//                if (string.IsNullOrEmpty(trimmedInclude)) continue;

//                var propertyNamesArray = trimmedInclude.Split('.');
//                var capitalizedPropertyNames = propertyNamesArray.Select(p => char.ToUpper(p[0]) + p.Substring(1)).ToList();
//                var propertyNames = string.Join(".", capitalizedPropertyNames);

//                // Check if the property exists in the entity, including nested properties
//                if (IsValidIncludePath(typeof(T), propertyNames))
//                {
//                    query = query.Include(propertyNames);
//                }
//                else
//                {
//                    var errors = new Dictionary<string, string[]>
//                    {
//                        { "Includes", new[] { $"Invalid include path: '{propertyNames}' does not exist in the entity '{typeof(T).Name}'." } },
//                    };
//                    throw new ValidationException(errors);
//                }
//            }
//            return query;
//        }

//        private static bool IsValidIncludePath(Type entityType, string includePath)
//        {
//            var properties = includePath.Split('.');
//            foreach (var property in properties)
//            {
//                var propertyInfo = entityType.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
//                if (propertyInfo == null)
//                {
//                    return false;
//                }
//                entityType = propertyInfo.PropertyType;
//                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(entityType) && entityType.IsGenericType)
//                {
//                    entityType = entityType.GetGenericArguments()[0];
//                }
//            }
//            return true;
//        }

//        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, Dictionary<string, string?> filters) where T : class
//        {
//            foreach (var filter in filters)
//            {
//                if (string.IsNullOrEmpty(filter.Value)) continue;

//                var filterParts = filter.Value.Split(':');
//                if (filterParts.Length != 2)
//                {
//                    var errors = new Dictionary<string, string[]>
//                    {
//                        {filter.Key, new[] { "Invalid filter format. The correct format is 'operator:value'." } },
//                    };
//                    throw new ValidationException(errors);
//                }

//                var operatorType = filterParts[0];
//                var value = filterParts[1];

//                query = ApplyFilter(query, filter.Key, operatorType, value);
//            }

//            return query;
//        }

//        private static IQueryable<T> ApplyFilter<T>(IQueryable<T> query, string property, string operatorType, string value) where T : class
//        {
//            switch (operatorType)
//            {
//                case "eq":
//                    query = query.Where(e => e != null && EF.Property<string>(e, property) == value);
//                    break;
//                case "neq":
//                    query = query.Where(e => e != null && EF.Property<string>(e, property) != value);
//                    break;
//                case "gt":
//                    query = query.Where(e => e != null && EF.Property<object>(e, property) != null && Convert.ToDouble(EF.Property<object>(e, property)) > Convert.ToDouble(value));
//                    break;
//                case "gte":
//                    query = query.Where(e => e != null && EF.Property<object>(e, property) != null && Convert.ToDouble(EF.Property<object>(e, property)) >= Convert.ToDouble(value));
//                    break;
//                case "lt":
//                    query = query.Where(e => e != null && EF.Property<object>(e, property) != null && Convert.ToDouble(EF.Property<object>(e, property)) < Convert.ToDouble(value));
//                    break;
//                case "lte":
//                    query = query.Where(e => e != null && EF.Property<object>(e, property) != null && Convert.ToDouble(EF.Property<object>(e, property)) <= Convert.ToDouble(value));
//                    break;
//                case "like":
//                    query = query.Where(e => e != null && EF.Property<string>(e, property) != null && EF.Functions.Like(EF.Property<string>(e, property), $"%{value}%"));
//                    break;
//                case "in":
//                    var values = value.Split(',').ToList();
//                    query = query.Where(e => e != null && EF.Property<string>(e, property) != null && values.Contains(EF.Property<string>(e, property)));
//                    break;
//                default:
//                    var errors = new Dictionary<string, string[]>
//                    {
//                        { property, new[] { $"Invalid filter operator: {operatorType}" } },
//                    };
//                    throw new ValidationException(errors);
//            }

//            return query;
//        }

//        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string? sortExpression) where T : class
//        {
//            if (string.IsNullOrEmpty(sortExpression)) return query;

//            var sortExpressions = sortExpression.Split(',');
//            foreach (var expression in sortExpressions)
//            {
//                var trimmedExpression = expression.Trim();
//                if (string.IsNullOrEmpty(trimmedExpression)) continue;

//                bool isDescending = trimmedExpression.StartsWith("-");
//                var propertyName = isDescending ? trimmedExpression.Substring(1) : trimmedExpression;

//                propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1);

//                // Check if the property exists
//                if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
//                {
//                    var errors = new Dictionary<string, string[]>
//                    {
//                        { "Sort", new[] { $"Invalid sort property: '{propertyName}' does not exist in the entity '{typeof(T).Name}'." } },
//                    };
//                    throw new ValidationException(errors);
//                }

//                query = isDescending
//                    ? query.OrderByDescending(e => EF.Property<object>(e, propertyName))
//                    : query.OrderBy(e => EF.Property<object>(e, propertyName));
//            }

//            return query;
//        }

//        public static async Task<PaginatedResult<T>> ApplyPaginationAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
//        {
//            var totalItems = await query.CountAsync();
//            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

//            return new PaginatedResult<T>(items, totalItems, page, pageSize);
//        }
//    }
//}
