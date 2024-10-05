using zity.DTOs.Services;
using zity.Models;

namespace zity.Mappers
{
    public class ServiceMapper
    {
        // ToDTO
        public static ServiceDTO ToDTO(Service service) =>
            new ServiceDTO
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price
            };

        // ToModelFromCreate
        public static Service ToModelFromCreate(ServiceCreateDTO serviceCreateDTO) =>
            new Service
            {
                Name = serviceCreateDTO.Name,
                Description = serviceCreateDTO.Description,
                Price = serviceCreateDTO.Price
            };

        // UpdateModelFromUpdate
        public static Service UpdateModelFromUpdate(Service service, ServiceUpdateDTO serviceUpdateDTO)
        {
            service.Name = serviceUpdateDTO.Name;
            service.Description = serviceUpdateDTO.Description;
            service.Price = serviceUpdateDTO.Price;

            return service;
        }

        // PatchModelFromPatch
        public static Service PatchModelFromPatch(Service service, ServicePatchDTO servicePatchDTO)
        {
            if (servicePatchDTO.Name != null)
            {
                service.Name = servicePatchDTO.Name;
            }

            if (servicePatchDTO.Description != null)
            {
                service.Description = servicePatchDTO.Description;
            }

            if (servicePatchDTO.Price != null)
            {
                service.Price = servicePatchDTO.Price.Value;
            }

            return service;
        }

    }
}
