using zity.DTOs.Services;
using zity.Mappers;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class ServiceService(IServiceRepository serviceRepository) : IServiceService
    {
        private readonly IServiceRepository _serviceRepository = serviceRepository;

        public async Task<PaginatedResult<ServiceDTO>> GetAllAsync(ServiceQueryDTO queryParam)
        {
            var pageServices = await _serviceRepository.GetAllAsync(queryParam);
            return new PaginatedResult<ServiceDTO>(
                pageServices.Contents.Select(ServiceMapper.ToDTO).ToList(),
                pageServices.TotalItems,
                pageServices.Page,
                pageServices.PageSize);
        }


        public async Task<ServiceDTO?> GetByIdAsync(int id, string? includes)
        {
            var service = await _serviceRepository.GetByIdAsync(id, includes);
            return service != null ? ServiceMapper.ToDTO(service) : null;
        }
        public async Task<ServiceDTO> CreateAsync(ServiceCreateDTO createDTO)
        {
            var service = ServiceMapper.ToModelFromCreate(createDTO);
            return ServiceMapper.ToDTO(await _serviceRepository.CreateAsync(service));
        }
        public async Task<ServiceDTO?> UpdateAsync(int id, ServiceUpdateDTO updateDTO)
        {
            var existingService = await _serviceRepository.GetByIdAsync(id, null);
            if (existingService == null)
            {
                return null;
            }

            ServiceMapper.UpdateModelFromUpdate(existingService, updateDTO);
            var updatedService = await _serviceRepository.UpdateAsync(existingService);
            return ServiceMapper.ToDTO(updatedService);
        }

        public async Task<ServiceDTO?> PatchAsync(int id, ServicePatchDTO patchDTO)
        {
            var existingService = await _serviceRepository.GetByIdAsync(id, null);
            if (existingService == null)
            {
                return null;
            }

            ServiceMapper.PatchModelFromPatch(existingService, patchDTO);
            var patchedService = await _serviceRepository.UpdateAsync(existingService);
            return ServiceMapper.ToDTO(patchedService);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _serviceRepository.DeleteAsync(id);
        }
    }
}
