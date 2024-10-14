using AutoMapper;
using zity.DTOs.Services;
using zity.Models;

namespace zity.Mappers
{
    public class ServiceMapping : Profile
    {
        public ServiceMapping()
        {
            CreateMap<Service, ServiceDTO>();

            CreateMap<ServiceCreateDTO, Service>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ServiceUpdateDTO, Service>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ServicePatchDTO, Service>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
