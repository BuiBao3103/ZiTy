using zity.Models;

namespace zity.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByValueAsync(string token);
        Task AddAsync(RefreshToken refreshToken);
        Task UpdateAsync(RefreshToken refreshToken);
        Task RevokeAsync(RefreshToken refreshToken);
        Task<List<RefreshToken>> GetUserRefreshTokensAsync(int userId);
    }
}
