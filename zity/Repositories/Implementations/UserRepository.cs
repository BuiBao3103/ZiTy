using Microsoft.EntityFrameworkCore;
using ZiTy.Data;
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
        public async Task<List<User>> GetAllAsync()
        {
            var users = _dbContext.Users.Where(u => u.DeletedAt == null);
            return await users.ToListAsync();
        }
    }
}
