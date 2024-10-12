using zity.DTOs.Apartments;
using zity.Mappers;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class ApartmentService(IApartmentRepository apartmentRepository) : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository = apartmentRepository;

        public async Task<ApartmentDTO> CreateAsync(ApartmentCreateDTO apartmentCreateDTO)
        {
            var apartment = ApartmentMapper.ToModelFromCreate(apartmentCreateDTO);
            return ApartmentMapper.ToDTO(await _apartmentRepository.CreateAsync(apartment));
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _apartmentRepository.DeleteAsync(id);
        }

        public async Task<PaginatedResult<ApartmentDTO>> GetAllAsync(ApartmentQueryDTO query)
        {
            var pageApartments = await _apartmentRepository.GetAllAsync(query);
            return new PaginatedResult<ApartmentDTO>(
                pageApartments.Contents.Select(ApartmentMapper.ToDTO).ToList(),
                pageApartments.TotalItems,
                pageApartments.Page,
                pageApartments.PageSize);
        }

        public async Task<ApartmentDTO?> GetByIdAsync(string id, string? includes)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(id, includes);
            return apartment != null ? ApartmentMapper.ToDTO(apartment) : null;
        }

        public async Task<ApartmentDTO?> PatchAsync(string id, ApartmentPatchDTO apartmentPatchDTO)
        {
            var existingApartment = await _apartmentRepository.GetByIdAsync(id, null);
            if (existingApartment == null)
            {
                return null;
            }
            ApartmentMapper.PatchModelFromPatch(existingApartment, apartmentPatchDTO);
            var patchedApartment = await _apartmentRepository.UpdateAsync(existingApartment);
            return ApartmentMapper.ToDTO(patchedApartment);
        }

        public async Task<ApartmentDTO?> UpdateAsync(string id, ApartmentUpdateDTO apartmentUpdateDTO)
        {
            var existingApartment = await _apartmentRepository.GetByIdAsync(id, null);
            if (existingApartment == null)
            {
                return null;
            }
            ApartmentMapper.UpdateModelFromUpdate(existingApartment, apartmentUpdateDTO);
            var updatedApartment = await _apartmentRepository.UpdateAsync(existingApartment);
            return ApartmentMapper.ToDTO(updatedApartment);
        }
    }
}
