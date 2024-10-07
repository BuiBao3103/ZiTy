using zity.DTOs.Questions;
using zity.Models;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<PaginatedResult<QuestionDTO>> GetAllAsync(QuestionQueryDTO query);
        Task<QuestionDTO?> GetByIdAsync(int id, string? includes);
        Task<QuestionDTO> CreateAsync(QuestionCreateDTO questionCreateDTO);
        Task<QuestionDTO?> UpdateAsync(int id, QuestionUpdateDTO questionUpdateDTO);
        Task<QuestionDTO?> PatchAsync(int id, QuestionPatchDTO questionPatchDTO);
        Task<bool> DeleteAsync(int id);
    }
}
