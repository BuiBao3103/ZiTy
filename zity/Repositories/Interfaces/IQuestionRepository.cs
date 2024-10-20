using zity.DTOs.Questions;
using zity.DTOs.Users;
using zity.Models;
using zity.Utilities;

namespace zity.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task<PaginatedResult<Question>> GetAllAsync(QuestionQueryDTO query);
        Task<Question?> GetByIdAsync(int id, string? includes);
        Task<Question> CreateAsync(Question question);
        Task<Question> UpdateAsync(Question question);
        Task<bool> DeleteAsync(int id);
    }
}
