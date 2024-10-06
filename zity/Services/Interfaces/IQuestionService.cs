using zity.DTOs.Questions;
using zity.Models;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<PaginatedResult<QuestionDTO>> GetAllAsync(QuestionQueryDTO query);
        Task<QuestionDTO?> GetByIdAsync(int id, string? includes);
        Task<QuestionDTO> CreateAsync(QuestionCreateDTO QuestionCreateDTO);
        Task<QuestionDTO?> UpdateAsync(int id, QuestionUpdateDTO QuestionUpdateDTO);
        Task<QuestionDTO?> PatchAsync(int id, QuestionPatchDTO QuestionPatchDTO);
        Task<bool> DeleteAsync(int id);
    }
}
