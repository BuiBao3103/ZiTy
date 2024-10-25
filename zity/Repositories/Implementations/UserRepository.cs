using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.DTOs.Users;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Utilities;
namespace zity.Services.Implementations
{
    public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<PaginatedResult<User>> GetAllAsync(UserQueryDTO queryParam)
        {
            var filterParams = new Dictionary<string, string?>
        {
            { "Id", queryParam.Id },
            { "Username", queryParam.Username }
        };

            var userQuery = _dbContext.Users
                .Where(u => u.DeletedAt == null)
                .ApplyIncludes(queryParam.Includes)
                .ApplyFilters(filterParams)
                .ApplySorting(queryParam.Sort)
                .ApplyPaginationAsync(queryParam.Page, queryParam.PageSize);

            return await userQuery;
        }

        public async Task<User?> GetByIdAsync(int id, string? includes = null)
        {
            var usersQuery = _dbContext.Users.Where(u => u.DeletedAt == null)
                .ApplyIncludes(includes);
            return await usersQuery.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> CreateAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users
                //.Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}

