using Microsoft.EntityFrameworkCore;
using zity.DTOs.Users;
using zity.Utilities;
using ZiTy.Data;
using ZiTy.Models;
using ZiTy.Repositories.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ZiTy.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PaginatedResult<User>> GetAllAsync(UserQueryDto query)
        {
            var usersQuery = _dbContext.Users.Where(u => u.DeletedAt == null);
            var totalItems = await usersQuery.CountAsync();
            var users = await usersQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
            return new PaginatedResult<User>(users, totalItems, query.Page, query.PageSize);
        }

    }
}
