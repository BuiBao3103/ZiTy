using Survey.Application.DTOs;
using Survey.Application.DTOs.OtherAnswers;

namespace Survey.Application.Interfaces;

public interface IOtherAnswerService
{
    Task<PaginatedResult<OtherAnswerDTO>> GetAllAsync(OtherAnswerQueryDTO query);
    Task<OtherAnswerDTO> GetByIdAsync(int id, string? includes = null);
    Task<OtherAnswerDTO> CreateAsync(OtherAnswerCreateDTO otherAnswerCreateDTO);
    Task<OtherAnswerDTO> UpdateAsync(int id, OtherAnswerUpdateDTO otherAnswerUpdateDTO);
    Task<OtherAnswerDTO> PatchAsync(int id, OtherAnswerPatchDTO otherAnswerPatchDTO);
    Task DeleteAsync(int id);
}
