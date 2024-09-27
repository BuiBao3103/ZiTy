using zity.Utilities;
using zity.Data;
using zity.DTOs.Users;
using zity.Models;
using zity.Repositories.Interfaces;

namespace zity.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FilterHandler<User> _filterHandler;
        private readonly SortHandler<User> _sortHandler;
        private readonly PaginationHandler<User> _paginationHandler;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _filterHandler = new FilterHandler<User>();
            _sortHandler = new SortHandler<User>();
            _paginationHandler = new PaginationHandler<User>();
        }


        public async Task<PaginatedResult<User>> GetAllAsync(UserQueryDto query)
        {
            var usersQuery = _dbContext.Users.Where(u => u.DeletedAt == null);

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
