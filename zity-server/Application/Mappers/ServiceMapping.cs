using Application.DTOs.Services;
using AutoMapper;
using Domain.Entities;


namespace Application.Mappers;

public class ServiceMapping : Profile
{
    public ServiceMapping()
    {
        CreateMap<Service, ServiceDTO>()
         .ForMember(dest => dest.BillDetails, opt => opt.MapFrom(src => src.BillDetails));

        CreateMap<ServiceCreateDTO, Service>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<ServiceUpdateDTO, Service>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<ServicePatchDTO, Service>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Name, opt => opt.Condition((src, dest, name) => name != null))
            .ForMember(dest => dest.Description, opt => opt.Condition((src, dest, description) => description != null))
            .ForMember(dest => dest.Price, opt => opt.Condition((src, dest, price) => price != 0));
    }
}

