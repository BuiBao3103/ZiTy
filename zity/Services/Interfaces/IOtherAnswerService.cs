using zity.DTOs.OtherAnswers;
using zity.Models;
using zity.Utilities;


namespace zity.Services.Interfaces
{
    public interface IOtherAnswerService
    {
        Task<PaginatedResult<OtherAnswerDTO>> GetAllAsync(OtherAnswerQueryDTO query);
        Task<OtherAnswerDTO?> GetByIdAsync(int id, string? includes);
        Task<OtherAnswerDTO> CreateAsync(OtherAnswerCreateDTO OtherAnswerCreateDTO);
        Task<OtherAnswerDTO?> UpdateAsync(int id, OtherAnswerUpdateDTO OtherAnswerUpdateDTO);
        Task<OtherAnswerDTO?> PatchAsync(int id, OtherAnswerPatchDTO OtherAnswerPatchDTO);
        Task<bool> DeleteAsync(int id);
    }
}
