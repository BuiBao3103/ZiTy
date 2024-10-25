using zity.DTOs.BillDetails;
using zity.Models;
using zity.Utilities;

namespace zity.Repositories.Interfaces
{
    public interface IBillDetailRepository
    {
        Task<PaginatedResult<BillDetail>> GetAllAsync(BillDetailQueryDTO queryParam);

        Task<BillDetail?> GetByIdAsync(int id, string? includes = null);

        Task<BillDetail> CreateAsync(BillDetail billDetail);

        Task<BillDetail> UpdateAsync(BillDetail billDetail);

        Task DeleteAsync(int id);
    }
}
