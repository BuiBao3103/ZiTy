using Apartment.Application.DTOs.Apartments;
using AutoMapper;
using Apartment.Domain.Entities;

namespace Apartment.Application.Mappers;

public class ApartmentMapping : Profile
{
    public ApartmentMapping()
    {
        CreateMap<Apartment.Domain.Entities.Apartment, ApartmentDTO>()
            .ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships));

        CreateMap<ApartmentCreateDTO, Apartment.Domain.Entities.Apartment>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<ApartmentUpdateDTO, Apartment.Domain.Entities.Apartment>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<ApartmentPatchDTO, Apartment.Domain.Entities.Apartment>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Area, opt => opt.Condition((src, dest) => src.Area != null))
            .ForMember(dest => dest.Description, opt => opt.Condition((src, dest) => src.Description != null))
            .ForMember(dest => dest.FloorNumber, opt => opt.Condition((src, dest) => src.FloorNumber != null))
            .ForMember(dest => dest.ApartmentNumber, opt => opt.Condition((src, dest) => src.ApartmentNumber != null))
            .ForMember(dest => dest.Status, opt => opt.Condition((src, dest) => src.Status != null));
    }
}