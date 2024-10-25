using zity.DTOs.UserAnswers;
using zity.Models;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IUserAnswerService
    {
        Task<PaginatedResult<UserAnswerDTO>> GetAllAsync(UserAnswerQueryDTO query);
        Task<UserAnswerDTO> GetByIdAsync(int id, string? includes);
        Task<UserAnswerDTO> CreateAsync(UserAnswerCreateDTO userAnswerCreateDTO);
        Task<UserAnswerDTO> UpdateAsync(int id, UserAnswerUpdateDTO userAnswerUpdateDTO);
        Task<UserAnswerDTO> PatchAsync(int id, UserAnswerPatchDTO userAnswerPatchDTO);
        Task DeleteAsync(int id);
    }
}
