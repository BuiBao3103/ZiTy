using zity.DTOs.OtherAnswers;
using zity.Models;
using zity.Utilities;


namespace zity.Services.Interfaces
{
    public interface IOtherAnswerService
    {
        Task<PaginatedResult<OtherAnswerDTO>> GetAllAsync(OtherAnswerQueryDTO query);
        Task<OtherAnswerDTO?> GetByIdAsync(int id, string? includes);
        Task<OtherAnswerDTO> CreateAsync(OtherAnswerCreateDTO otherAnswerCreateDTO);
        Task<OtherAnswerDTO?> UpdateAsync(int id, OtherAnswerUpdateDTO otherAnswerUpdateDTO);
        Task<OtherAnswerDTO?> PatchAsync(int id, OtherAnswerPatchDTO otherAnswerPatchDTO);
        Task<bool> DeleteAsync(int id);
    }
}
