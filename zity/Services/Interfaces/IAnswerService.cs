using zity.DTOs.Answers;
using zity.Models;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IAnswerService
    {
        Task<PaginatedResult<AnswerDTO>> GetAllAsync(AnswerQueryDTO query);
        Task<AnswerDTO> GetByIdAsync(int id, string? includes);
        Task<AnswerDTO> CreateAsync(AnswerCreateDTO answerCreateDTO);
        Task<AnswerDTO> UpdateAsync(int id, AnswerUpdateDTO answerUpdateDTO);
        Task<AnswerDTO> PatchAsync(int id, AnswerPatchDTO answerPatchDTO);
        Task DeleteAsync(int id);
    }
}
