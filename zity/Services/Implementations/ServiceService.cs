using AutoMapper;
using zity.DTOs.Services;
using zity.ExceptionHandling.Exceptions;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class ServiceService(IServiceRepository serviceRepository, IMapper mapper) : IServiceService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IServiceRepository _serviceRepository = serviceRepository;

        public async Task<PaginatedResult<ServiceDTO>> GetAllAsync(ServiceQueryDTO queryParam)
        {
            var pageServices = await _serviceRepository.GetAllAsync(queryParam);
            var services = pageServices.Contents.Select(_mapper.Map<ServiceDTO>).ToList();

            return new PaginatedResult<ServiceDTO>(
                services,
                pageServices.TotalItems,
                pageServices.Page,
                pageServices.PageSize);
        }

        public async Task<ServiceDTO> GetByIdAsync(int id, string? includes = null)
        {
            var service = await _serviceRepository.GetByIdAsync(id, includes)
                    ?? throw new EntityNotFoundException(nameof(Service), id);
            return _mapper.Map<ServiceDTO>(service);
        }

        public async Task<ServiceDTO> CreateAsync(ServiceCreateDTO createDTO)
        {
            var service = _mapper.Map<Service>(createDTO);
            return _mapper.Map<ServiceDTO>(await _serviceRepository.CreateAsync(service));
        }

        public async Task<ServiceDTO> UpdateAsync(int id, ServiceUpdateDTO updateDTO)
        {
            var existingService = await _serviceRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(Report), id);
            _mapper.Map(updateDTO, existingService);
            var updatedService = await _serviceRepository.UpdateAsync(existingService);
            return _mapper.Map<ServiceDTO>(updatedService);
        }

        public async Task<ServiceDTO> PatchAsync(int id, ServicePatchDTO patchDTO)
        {
            var existingService = await _serviceRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(Report), id);
            _mapper.Map(patchDTO, existingService);
            var patchedService = await _serviceRepository.UpdateAsync(existingService);
            return _mapper.Map<ServiceDTO>(patchedService);
        }

        public async Task DeleteAsync(int id)
        {
            await _serviceRepository.DeleteAsync(id);
        }
    }
}
