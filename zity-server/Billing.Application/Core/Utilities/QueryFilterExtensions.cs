using Billing.Application.DTOs;
using System.Linq.Expressions;

namespace Billing.Application.Core.Utilities;

public static class FilterCriteriaExtensions
{
    public static Expression<Func<T, bool>> BuildFilterCriteria<T>(
        this object queryDto,
        Expression<Func<T, bool>> baseFilter = null)
    {
        // If no query DTO, return just the base filter or "always true"
        if (queryDto == null)
            return baseFilter ?? (x => true);

        var properties = queryDto.GetType().GetProperties()
            .Where(p => p.GetValue(queryDto) != null &&
                       !typeof(BaseQueryDTO).GetProperties().Select(bp => bp.Name)
                           .Contains(p.Name));
        Console.WriteLine(properties.ToList().Count);
        var parameter = Expression.Parameter(typeof(T), "x");
        Expression finalExpression = Expression.Constant(true);

        // Build dynamic filter criteria
        foreach (var property in properties)
        {
            var filterValue = property.GetValue(queryDto)?.ToString();
            Console.WriteLine(filterValue);
            if (string.IsNullOrEmpty(filterValue)) continue;

            var parts = filterValue.Split(':');
            if (parts.Length != 2) continue;
            Console.WriteLine(1);
            var operator_ = parts[0].ToLower();
            var value = parts[1];

            var propertyName = property.Name;
            var propertyInfo = typeof(T).GetProperty(propertyName);

            if (propertyInfo == null) continue;
            Console.WriteLine(2);
            var propertyExpression = Expression.Property(parameter, propertyInfo);
            Expression condition = null;
            try
            {
                switch (operator_)
                {
                    case "eq":
                        var equalValue = ConvertToNullableType(value, propertyInfo.PropertyType);
                        condition = Expression.Equal(propertyExpression, Expression.Constant(equalValue, propertyInfo.PropertyType));
                        break;

                    case "neq":
                        var notEqualValue = ConvertToNullableType(value, propertyInfo.PropertyType);
                        condition = Expression.NotEqual(propertyExpression, Expression.Constant(notEqualValue, propertyInfo.PropertyType));
                        break;

                    case "gt":
                        var gtValue = ConvertToNullableType(value, propertyInfo.PropertyType);
                        condition = Expression.GreaterThan(propertyExpression, Expression.Constant(gtValue, propertyInfo.PropertyType));
                        break;

                    case "gte":
                        var gteValue = ConvertToNullableType(value, propertyInfo.PropertyType);
                        condition = Expression.GreaterThanOrEqual(propertyExpression, Expression.Constant(gteValue, propertyInfo.PropertyType));
                        break;

                    case "lt":
                        var ltValue = ConvertToNullableType(value, propertyInfo.PropertyType);
                        condition = Expression.LessThan(propertyExpression, Expression.Constant(ltValue, propertyInfo.PropertyType));
                        break;

                    case "lte":
                        var lteValue = ConvertToNullableType(value, propertyInfo.PropertyType);
                        condition = Expression.LessThanOrEqual(propertyExpression, Expression.Constant(lteValue, propertyInfo.PropertyType));
                        break;

                    case "like":
                        if (propertyInfo.PropertyType == typeof(string))
                        {
                            var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            condition = Expression.Call(propertyExpression, methodInfo, Expression.Constant(value));
                        }
                        break;

                    case "in":
                        var values = value.Split(',')
                            .Select(v => ConvertToNullableType(v.Trim(), propertyInfo.PropertyType))
                            .ToList();
                        var listType = typeof(List<>).MakeGenericType(propertyInfo.PropertyType);
                        var containsMethod = typeof(Enumerable).GetMethods()
                            .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                            .MakeGenericMethod(propertyInfo.PropertyType);
                        condition = Expression.Call(null, containsMethod, Expression.Constant(values), propertyExpression);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error building filter criteria for property {propertyName}", ex);
                // Skip invalid conversions
                //continue;
            }

            if (condition != null)
            {
                Console.WriteLine($"add: {Expression.Constant(condition.ToString())}");
                finalExpression = Expression.AndAlso(finalExpression, condition);
            }
        }

        // If there's a base filter, combine it with the dynamic criteria
        if (baseFilter != null)
        {
            // Convert the base filter to use our parameter
            var visitor = new ParameterReplaceVisitor(baseFilter.Parameters[0], parameter);
            var baseBody = visitor.Visit(baseFilter.Body);

            // Combine with AND
            finalExpression = Expression.AndAlso(baseBody, finalExpression);
        }

        return Expression.Lambda<Func<T, bool>>(finalExpression, parameter);
    }

    // Helper visitor class to replace parameters in expressions
    private class ParameterReplaceVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;

        public ParameterReplaceVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _oldParameter ? _newParameter : base.VisitParameter(node);
        }
    }
    private static object ConvertToNullableType(string value, Type targetType)
    {
        if (string.IsNullOrEmpty(value))
            return null; // Return null for empty strings

        var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

        return Convert.ChangeType(value, underlyingType);
    }
}
