using Survey.Application.DTOs;
using Survey.Application.DTOs.Questions;

namespace Survey.Application.Interfaces;

public interface IQuestionService
{
    Task<PaginatedResult<QuestionDTO>> GetAllAsync(QuestionQueryDTO query);
    Task<QuestionDTO> GetByIdAsync(int id, string? includes = null);
    Task<QuestionDTO> CreateAsync(QuestionCreateDTO questionCreateDTO);
    Task<QuestionDTO> UpdateAsync(int id, QuestionUpdateDTO questionUpdateDTO);
    Task<QuestionDTO> PatchAsync(int id, QuestionPatchDTO questionPatchDTO);
    Task DeleteAsync(int id);
}
