using zity.DTOs.Apartments;
using zity.Utilities;

namespace zity.Services.Interfaces
{
    public interface IApartmentService
    {
        Task<PaginatedResult<ApartmentDTO>> GetAllAsync(ApartmentQueryDTO query);
        Task<ApartmentDTO?> GetByIdAsync(string id, string? includes);
        Task<ApartmentDTO> CreateAsync(ApartmentCreateDTO apartmentCreateDTO);
        Task<ApartmentDTO?> UpdateAsync(string id, ApartmentUpdateDTO apartmentUpdateDTO);
        Task<ApartmentDTO?> PatchAsync(string id, ApartmentPatchDTO apartmentPatchDTO);
        Task<bool> DeleteAsync(string id);
    }
}
