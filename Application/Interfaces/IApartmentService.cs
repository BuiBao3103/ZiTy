using Application.DTOs;
using Application.DTOs.Apartments;

namespace Application.Interfaces;

public interface IApartmentService
{
    Task<PaginatedResult<ApartmentDTO>> GetAllAsync(ApartmentQueryDTO query);
    Task<ApartmentDTO> GetByIdAsync(string id, string? includes = null);
    Task<ApartmentDTO> CreateAsync(ApartmentCreateDTO apartmentCreateDTO);
    Task<ApartmentDTO> UpdateAsync(string id, ApartmentUpdateDTO apartmentUpdateDTO);
    Task<ApartmentDTO> PatchAsync(string id, ApartmentPatchDTO apartmentPatchDTO);
    Task DeleteAsync(string id);
}
