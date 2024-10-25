using zity.DTOs.Answers;
using zity.Models;
using zity.Utilities;

namespace zity.Repositories.Interfaces
{
    public interface IAnswerRepository
    {
        Task<PaginatedResult<Answer>> GetAllAsync(AnswerQueryDTO query);
        Task<Answer?> GetByIdAsync(int id, string? includes = null);
        Task<Answer> CreateAsync(Answer answer);
        Task<Answer> UpdateAsync(Answer answer);
        Task DeleteAsync(int id);
    }
}
