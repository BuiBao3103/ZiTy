using Application.DTOs;
using Application.DTOs.Services;

namespace Application.Services;

public interface IServiceService
{
    Task<PaginatedResult<ServiceDTO>> GetAllAsync(ServiceQueryDTO query);
    Task<ServiceDTO> GetByIdAsync(int id, string? includes = null);
    Task<ServiceDTO> CreateAsync(ServiceCreateDTO serviceCreateDTO);
    Task<ServiceDTO> UpdateAsync(int id, ServiceUpdateDTO serviceUpdateDTO);
    Task<ServiceDTO> PatchAsync(int id, ServicePatchDTO servicePatchDTO);
    Task DeleteAsync(int id);
}

