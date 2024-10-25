using zity.DTOs.RejectionReasons;
using zity.Models;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IRejectionReasonService
    {
        Task<PaginatedResult<RejectionReasonDTO>> GetAllAsync(RejectionReasonQueryDTO query);
        Task<RejectionReasonDTO> GetByIdAsync(int id, string? includes = null);
        Task<RejectionReasonDTO> CreateAsync(RejectionReasonCreateDTO rejectionReasonCreateDTO);
        Task<RejectionReasonDTO> UpdateAsync(int id, RejectionReasonUpdateDTO rejectionReasonUpdateDTO);
        Task<RejectionReasonDTO> PatchAsync(int id, RejectionReasonPatchDTO rejectionReasonPatchDTO);
        Task DeleteAsync(int id);
    }
}
