using zity.DTOs.OtherAnswers;
using zity.Models;
using zity.Utilities;

namespace zity.Repositories.Interfaces
{
    public interface IOtherAnswerRepository
    {
        Task<PaginatedResult<OtherAnswer>> GetAllAsync(OtherAnswerQueryDTO query);
        Task<OtherAnswer?> GetByIdAsync(int id, string? includes);
        Task<OtherAnswer> CreateAsync(OtherAnswer otherAnswer);
        Task<OtherAnswer> UpdateAsync(OtherAnswer otherAnswer);
        Task DeleteAsync(int id);
    }
}
