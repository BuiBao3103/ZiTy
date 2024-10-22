using Microsoft.EntityFrameworkCore;
using zity.Data;
using zity.Models;
using zity.Repositories.Interfaces;

namespace zity.Repositories.Implementations
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetByValueAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .SingleOrDefaultAsync(rt => rt.Token == token && !rt.IsRevoked);
        }

        public async Task<List<RefreshToken>> GetAllActiveTokensAsync()
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .Where(rt => !rt.IsRevoked && rt.ExpiryTime > DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task RevokeAsync(RefreshToken refreshToken)
        {
            refreshToken.IsRevoked = true;
            await UpdateAsync(refreshToken);
        }

        public async Task<List<RefreshToken>> GetUserRefreshTokensAsync(int userId)
        {
            return await _context.RefreshTokens
                .Where(rt => rt.UserId == userId)
                .ToListAsync();
        }
    }
}