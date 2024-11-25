using Application.DTOs;
using System.Linq.Expressions;

namespace Application.Core.Utilities;

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

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression finalExpression = Expression.Constant(true);

        // Build dynamic filter criteria
        foreach (var property in properties)
        {
            var filterValue = property.GetValue(queryDto)?.ToString();
            if (string.IsNullOrEmpty(filterValue)) continue;

            var parts = filterValue.Split(':');
            if (parts.Length != 2) continue;

            var operator_ = parts[0].ToLower();
            var value = parts[1];

            var propertyPath = property.Name.Split('_');
            Expression propertyExpression = parameter;

            // Build the property access chain for nested properties
            foreach (var prop in propertyPath)
            {
                var currentType = propertyExpression.Type;
                var propertyInfo = currentType.GetProperty(prop);

                if (propertyInfo == null)
                {
                    // Try to find case-insensitive match
                    propertyInfo = currentType.GetProperties()
                        .FirstOrDefault(p => p.Name.Equals(prop, StringComparison.OrdinalIgnoreCase));

                    if (propertyInfo == null) break;
                }

                propertyExpression = Expression.Property(propertyExpression, propertyInfo);
            }

            // If property path is invalid, skip this filter
            if (propertyExpression == parameter) continue;

            Expression condition = null;
            try
            {
                var propertyType = propertyExpression.Type;

                switch (operator_)
                {
                    case "eq":
                        var equalValue = ConvertToNullableType(value, propertyType);
                        condition = Expression.Equal(propertyExpression, Expression.Constant(equalValue, propertyType));
                        break;

                    case "neq":
                        var notEqualValue = ConvertToNullableType(value, propertyType);
                        condition = Expression.NotEqual(propertyExpression, Expression.Constant(notEqualValue, propertyType));
                        break;

                    case "gt":
                        var gtValue = ConvertToNullableType(value, propertyType);
                        condition = Expression.GreaterThan(propertyExpression, Expression.Constant(gtValue, propertyType));
                        break;

                    case "gte":
                        var gteValue = ConvertToNullableType(value, propertyType);
                        condition = Expression.GreaterThanOrEqual(propertyExpression, Expression.Constant(gteValue, propertyType));
                        break;

                    case "lt":
                        var ltValue = ConvertToNullableType(value, propertyType);
                        condition = Expression.LessThan(propertyExpression, Expression.Constant(ltValue, propertyType));
                        break;

                    case "lte":
                        var lteValue = ConvertToNullableType(value, propertyType);
                        condition = Expression.LessThanOrEqual(propertyExpression, Expression.Constant(lteValue, propertyType));
                        break;

                    case "like":
                        if (propertyType == typeof(string))
                        {
                            var methodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            condition = Expression.Call(propertyExpression, methodInfo, Expression.Constant(value));
                        }
                        else if (IsNumericType(propertyType))
                        {
                            // Convert property to string for numeric contains check
                            var toStringMethod = propertyType.GetMethod("ToString", Type.EmptyTypes);
                            var propertyString = Expression.Call(propertyExpression, toStringMethod);
                            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            condition = Expression.Call(propertyString, containsMethod, Expression.Constant(value));
                        }
                        break;

                    case "in":
                        var values = value.Split(',')
                            .Select(v => v.Trim())
                            .ToList();

                        if (IsNumericType(propertyType))
                        {
                            var convertedValues = values
                                .Select(v => ConvertToNullableType(v, propertyType))
                                .ToList();

                            var listType = typeof(List<>).MakeGenericType(propertyType);
                            var containsMethod = typeof(Enumerable).GetMethods()
                                .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                                .MakeGenericMethod(propertyType);
                            condition = Expression.Call(null, containsMethod, Expression.Constant(convertedValues), propertyExpression);
                        }
                        else
                        {
                            var listType = typeof(List<>).MakeGenericType(propertyType);
                            var containsMethod = typeof(Enumerable).GetMethods()
                                .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                                .MakeGenericMethod(propertyType);
                            condition = Expression.Call(null, containsMethod, Expression.Constant(values), propertyExpression);
                        }
                        break;
                }

                if (condition != null)
                {
                    // Add null checks for nested properties
                    if (propertyPath.Length > 1)
                    {
                        Expression nullCheckExpression = parameter;
                        for (int i = 0; i < propertyPath.Length - 1; i++)
                        {
                            nullCheckExpression = Expression.Property(nullCheckExpression, propertyPath[i]);
                            var nullCheck = Expression.NotEqual(nullCheckExpression, Expression.Constant(null));
                            condition = Expression.AndAlso(nullCheck, condition);
                        }
                    }

                    finalExpression = Expression.AndAlso(finalExpression, condition);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error building filter criteria for property {property.Name}", ex);
            }
        }

        // If there's a base filter, combine it with the dynamic criteria
        if (baseFilter != null)
        {
            var visitor = new ParameterReplaceVisitor(baseFilter.Parameters[0], parameter);
            var baseBody = visitor.Visit(baseFilter.Body);
            finalExpression = Expression.AndAlso(baseBody, finalExpression);
        }

        return Expression.Lambda<Func<T, bool>>(finalExpression, parameter);
    }

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
            return null;

        var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
        return Convert.ChangeType(value, underlyingType);
    }

    private static bool IsNumericType(Type type)
    {
        // Handle nullable numeric types
        if (Nullable.GetUnderlyingType(type) != null)
        {
            type = Nullable.GetUnderlyingType(type);
        }

        switch (Type.GetTypeCode(type))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return true;
            default:
                return false;
        }
    }
}