using zity.DTOs.Bills;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IBillService
    {
        Task<PaginatedResult<BillDTO>> GetAllAsync(BillQueryDTO queryParam);
        Task<BillDTO?> GetByIdAsync(int id, string? includes);
        Task<BillDTO> CreateAsync(BillCreateDTO billCreateDTO);
        Task<BillDTO?> UpdateAsync(int id, BillUpdateDTO billUpdateDTO);
        Task<BillDTO?> PatchAsync(int id, BillPatchDTO billPatchDTO);
        Task<bool> DeleteAsync(int id);
        Task<string> CreatePaymentAsync(int id);
    }
}