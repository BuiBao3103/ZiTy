using Microsoft.EntityFrameworkCore;
using zity.ExceptionHandling;
using zity.Utilities;
using ZiTy.Data;
using ZiTy.DTOs.Users;
using ZiTy.Models;
using ZiTy.Repositories.Interfaces;

namespace ZiTy.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // Apply sorting
        public async Task<PaginatedResult<User>> GetAllAsync(UserQueryDto query)
        {
            var usersQuery = _dbContext.Users.Where(u => u.DeletedAt == null);

            // Get the list of valid property names for the User entity
            var validProperties = typeof(User).GetProperties().Select(p => p.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

            // Apply sorting
            if (!string.IsNullOrEmpty(query.Sort))
            {
                var sortExpressions = query.Sort.Split(',');
                foreach (var sortExpression in sortExpressions)
                {
                    var trimmedExpression = sortExpression.Trim();
                    if (string.IsNullOrEmpty(trimmedExpression)) continue;

                    var isDescending = trimmedExpression.StartsWith("-");
                    var propertyName = isDescending ? trimmedExpression.Substring(1) : trimmedExpression;

                    // Ensure property name matches the case of the entity properties
                    propertyName = char.ToUpper(propertyName[0]) + propertyName.Substring(1);

                    // Check if the property name is valid
                    if (!validProperties.Contains(propertyName))
                    {
                        // Handle invalid property name (e.g., log an error, throw an exception, etc.)
                        throw new AppError($"Invalid sort property: {propertyName}");
                    }


                    usersQuery = isDescending
                        ? usersQuery.OrderByDescending(e => EF.Property<object>(e, propertyName))
                        : usersQuery.OrderBy(e => EF.Property<object>(e, propertyName));
                }
            }

            var totalItems = await usersQuery.CountAsync();
            var users = await usersQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PaginatedResult<User>(users, totalItems, query.Page, query.PageSize);
        }
    }
}
