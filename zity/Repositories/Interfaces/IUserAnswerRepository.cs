using zity.DTOs.UserAnswers;
using zity.Models;
using zity.Utilities;

namespace zity.Repositories.Interfaces
{
    public interface IUserAnswerRepository
    {
        Task<PaginatedResult<UserAnswer>> GetAllAsync(UserAnswerQueryDTO query);
        Task<UserAnswer?> GetByIdAsync(int id, string? includes);
        Task<UserAnswer> CreateAsync(UserAnswer userAnswer);
        Task<UserAnswer> UpdateAsync(UserAnswer userAnswer);
        Task<bool> DeleteAsync(int id);
    }
}
