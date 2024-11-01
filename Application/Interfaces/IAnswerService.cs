using Application.DTOs.Answers;
using Application.Core.Utilities;

namespace Application.Interfaces
{
    public interface IAnswerService
    {
        Task<PaginatedResult<AnswerDTO>> GetAllAsync(AnswerQueryDTO query);
        Task<AnswerDTO> GetByIdAsync(int id, string? includes = null);
        Task<AnswerDTO> CreateAsync(AnswerCreateDTO answerCreateDTO);
        Task<AnswerDTO> UpdateAsync(int id, AnswerUpdateDTO answerUpdateDTO);
        Task<AnswerDTO> PatchAsync(int id, AnswerPatchDTO answerPatchDTO);
        Task DeleteAsync(int id);
    }
}
