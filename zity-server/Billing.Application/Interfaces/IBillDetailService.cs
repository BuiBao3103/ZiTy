using Billing.Application.DTOs;
using Billing.Application.DTOs.BillDetails;

namespace Billing.Application.Interfaces;

public interface IBillDetailService
{
    Task<PaginatedResult<BillDetailDTO>> GetAllAsync(BillDetailQueryDTO queryParam);

    Task<BillDetailDTO> GetByIdAsync(int id, string? includes = null);

    Task<BillDetailDTO> CreateAsync(BillDetailCreateDTO createDTO);

    Task<BillDetailDTO> UpdateAsync(int id, BillDetailUpdateDTO updateDTO);

    Task<BillDetailDTO> PatchAsync(int id, BillDetailPatchDTO patchDTO);

    Task DeleteAsync(int id);
}

