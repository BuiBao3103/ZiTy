using zity.DTOs.Services;
using zity.Models;
using zity.Utilities;

namespace zity.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<PaginatedResult<Service>> GetAllAsync(ServiceQueryDTO query);
        Task<Service?> GetByIdAsync(int id, string? includes);
        Task<Service> CreateAsync(Service service);
        Task<Service> UpdateAsync(Service service);
        Task<bool> DeleteAsync(int id);
    }
}
