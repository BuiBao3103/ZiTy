using zity.DTOs.Services;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IServiceService
    {
        Task<PaginatedResult<ServiceDTO>> GetAllAsync(ServiceQueryDTO query);
        Task<ServiceDTO> GetByIdAsync(int id, string? includes = null);
        Task<ServiceDTO> CreateAsync(ServiceCreateDTO serviceCreateDTO);
        Task<ServiceDTO> UpdateAsync(int id, ServiceUpdateDTO serviceUpdateDTO);
        Task<ServiceDTO> PatchAsync(int id, ServicePatchDTO servicePatchDTO);
        Task DeleteAsync(int id);
    }
}
