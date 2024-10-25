using zity.DTOs.RejectionReasons;
using zity.Models;
using zity.Utilities;

namespace zity.Repositories.Interfaces
{
    public interface IRejectionReasonRepository
    {
        Task<PaginatedResult<RejectionReason>> GetAllAsync(RejectionReasonQueryDTO query);
        Task<RejectionReason?> GetByIdAsync(int id, string? includes);
        Task<RejectionReason> CreateAsync(RejectionReason rejectionReason);
        Task<RejectionReason> UpdateAsync(RejectionReason rejectionReason);
        Task DeleteAsync(int id);
    }
}
