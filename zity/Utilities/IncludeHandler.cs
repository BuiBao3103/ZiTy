using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using zity.ExceptionHandling;

namespace zity.Utilities
{
    public class IncludeHandler<T> where T : class
    {
        public IQueryable<T> ApplyIncludes(IQueryable<T> query, string includes)
        {
            string[] includesArray = includes.Split(',');

            foreach (var include in includesArray)
            {
                var trimmedInclude = include.Trim();
                if (string.IsNullOrEmpty(trimmedInclude)) continue;

                var propertyNamesArray = trimmedInclude.Split('.');
                var capitalizedPropertyNames = propertyNamesArray.Select(p => char.ToUpper(p[0]) + p.Substring(1)).ToList();
                var propertyNames = string.Join(".", capitalizedPropertyNames);

                // Check if the property exists in the entity, including nested properties
                if (IsValidIncludePath(typeof(T), propertyNames))
                {
                    query = query.Include(propertyNames);
                }
                else
                {
                    throw new AppError($"Invalid include path: '{propertyNames}' does not exist in the entity '{typeof(T).Name}'.");
                }
            }
            return query;
        }

        private bool IsValidIncludePath(Type entityType, string includePath)
        {
            var properties = includePath.Split('.');
            foreach (var property in properties)
            {
                var propertyInfo = entityType.GetProperty(property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                {
                    return false;
                }
                entityType = propertyInfo.PropertyType;
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(entityType) && entityType.IsGenericType)
                {
                    entityType = entityType.GetGenericArguments()[0];
                }
            }
            return true;
        }
    }
}
