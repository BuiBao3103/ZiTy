using AutoMapper;
using zity.DTOs.Apartments;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IMapper _mapper;

        public ApartmentService(IApartmentRepository apartmentRepository, IMapper mapper)
        {
            _apartmentRepository = apartmentRepository;
            _mapper = mapper;
        }

        public async Task<ApartmentDTO> CreateAsync(ApartmentCreateDTO apartmentCreateDTO)
        {
            var apartment = _mapper.Map<Apartment>(apartmentCreateDTO);
            var createdApartment = await _apartmentRepository.CreateAsync(apartment);
            return _mapper.Map<ApartmentDTO>(createdApartment);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _apartmentRepository.DeleteAsync(id);
        }

        public async Task<PaginatedResult<ApartmentDTO>> GetAllAsync(ApartmentQueryDTO query)
        {
            var pageApartments = await _apartmentRepository.GetAllAsync(query);
            var apartments = pageApartments.Contents.Select(_mapper.Map<ApartmentDTO>).ToList();
            return new PaginatedResult<ApartmentDTO>(
                apartments,
                pageApartments.TotalItems,
                pageApartments.Page,
                pageApartments.PageSize);
        }

        public async Task<ApartmentDTO?> GetByIdAsync(string id, string? includes)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(id, includes);
            return apartment != null ? _mapper.Map<ApartmentDTO>(apartment) : null;
        }

        public async Task<ApartmentDTO?> PatchAsync(string id, ApartmentPatchDTO apartmentPatchDTO)
        {
            var existingApartment = await _apartmentRepository.GetByIdAsync(id, null);
            if (existingApartment == null)
            {
                return null;
            }

            _mapper.Map(apartmentPatchDTO, existingApartment);
            var patchedApartment = await _apartmentRepository.UpdateAsync(existingApartment);
            return _mapper.Map<ApartmentDTO>(patchedApartment);
        }

        public async Task<ApartmentDTO?> UpdateAsync(string id, ApartmentUpdateDTO apartmentUpdateDTO)
        {
            var existingApartment = await _apartmentRepository.GetByIdAsync(id, null);
            if (existingApartment == null)
            {
                return null;
            }

            _mapper.Map(apartmentUpdateDTO, existingApartment);
            var updatedApartment = await _apartmentRepository.UpdateAsync(existingApartment);
            return _mapper.Map<ApartmentDTO>(updatedApartment);
        }
    }
}
