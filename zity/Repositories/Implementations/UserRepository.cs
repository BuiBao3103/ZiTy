using zity.Utilities;
using zity.Data;
using zity.DTOs.Users;
using zity.Models;
using zity.Repositories.Interfaces;

namespace zity.Repositories.Implementations
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IncludeHandler<User> _includeHandler = new();
        private readonly FilterHandler<User> _filterHandler = new();
        private readonly SortHandler<User> _sortHandler = new();
        private readonly PaginationHandler<User> _paginationHandler = new();

        public async Task<PaginatedResult<User>> GetAllAsync(UserQueryDTO query)
        {
            var usersQuery = _dbContext.Users.Where(u => u.DeletedAt == null);
            var userQuery = _includeHandler.ApplyIncludes(usersQuery, query.Includes);
            var filters = new Dictionary<string, string>
            {
                { "Id", query.Id },
                { "Username", query.Username }
            };

            usersQuery = _filterHandler.ApplyFilters(usersQuery, filters);
            usersQuery = _sortHandler.ApplySorting(usersQuery, query.Sort);

            return await _paginationHandler.ApplyPaginationAsync(usersQuery, query.Page, query.PageSize);
        }



    }
}
