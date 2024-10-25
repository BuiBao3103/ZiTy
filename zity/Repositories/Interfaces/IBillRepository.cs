using zity.DTOs.Bills;
using zity.Models;
using zity.Utilities;

namespace zity.Repositories.Interfaces
{
    public interface IBillRepository
    {
        Task<PaginatedResult<Bill>> GetAllAsync(BillQueryDTO queryParam);
        Task<Bill?> GetByIdAsync(int id, string? includes);
        Task<Bill> CreateAsync(Bill bill);
        Task<Bill> UpdateAsync(Bill bill);
        Task DeleteAsync(int id);
    }
}