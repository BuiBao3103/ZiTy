using zity.DTOs.BillDetails;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IBillDetailService
    {
        Task<PaginatedResult<BillDetailDTO>> GetAllAsync(BillDetailQueryDTO queryParam);

        Task<BillDetailDTO?> GetByIdAsync(int id, string? includes);

        Task<BillDetailDTO> CreateAsync(BillDetailCreateDTO createDTO);

        Task<BillDetailDTO?> UpdateAsync(int id, BillDetailUpdateDTO updateDTO);

        Task<BillDetailDTO?> PatchAsync(int id, BillDetailPatchDTO patchDTO);

        Task<bool> DeleteAsync(int id);
    }
}
