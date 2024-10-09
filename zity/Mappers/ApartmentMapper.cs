using zity.DTOs.Apartments;
using zity.Models;

namespace zity.Mappers
{
    public class ApartmentMapper
    {
        public static ApartmentDTO ToDTO(Apartment apartment)
        {
            return new ApartmentDTO
            {
                Id = apartment.Id,
                Area = apartment.Area,
                Description = apartment.Description,
                FloorNumber = apartment.FloorNumber,
                ApartmentNumber = apartment.ApartmentNumber,
                Status = apartment.Status,
                CreatedAt = apartment.CreatedAt,
                UpdatedAt = apartment.UpdatedAt,
            };
        }

        public static Apartment ToModelFromCreate(ApartmentCreateDTO apartmentCreateDTO)
        {
            return new Apartment
            {
                Id = apartmentCreateDTO.Id,
                Area = apartmentCreateDTO.Area,
                Description = apartmentCreateDTO.Description,
                FloorNumber = apartmentCreateDTO.FloorNumber,
                ApartmentNumber = apartmentCreateDTO.ApartmentNumber,
                Status = apartmentCreateDTO.Status,
                CreatedAt = DateTime.Now,
            };
        }

        public static Apartment UpdateModelFromUpdate(Apartment apartment, ApartmentUpdateDTO updateDTO)
        {
            apartment.Area = updateDTO.Area;
            apartment.Description = updateDTO.Description;
            apartment.FloorNumber = updateDTO.FloorNumber;
            apartment.ApartmentNumber = updateDTO.ApartmentNumber;
            apartment.Status = updateDTO.Status;
            apartment.UpdatedAt = DateTime.Now;
            return apartment;
        }

        public static Apartment PatchModelFromPatch(Apartment apartment, ApartmentPatchDTO patchDTO)
        {
            if (patchDTO.Area != 0)
                apartment.Area = patchDTO.Area;
            if (patchDTO.Description != null)
                apartment.Description = patchDTO.Description;
            if (patchDTO.FloorNumber != 0)
                apartment.FloorNumber = patchDTO.FloorNumber;
            if (patchDTO.ApartmentNumber != 0)
                apartment.ApartmentNumber = patchDTO.ApartmentNumber;
            apartment.UpdatedAt = DateTime.Now;
            return apartment;
        }
    }
}
