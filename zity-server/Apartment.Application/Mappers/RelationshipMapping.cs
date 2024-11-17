using Apartment.Application.DTOs.Relationships;
using AutoMapper;
using Apartment.Domain.Entities;

namespace Apartment.Application.Mappers;

public class RelationshipMapping : Profile
{
    public RelationshipMapping()
    {
        CreateMap<Relationship, RelationshipDTO>();

        CreateMap<RelationshipCreateDTO, Relationship>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<RelationshipUpdateDTO, Relationship>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<RelationshipPatchDTO, Relationship>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ApartmentId, opt => opt.MapFrom(src => src.ApartmentId));
    }
}

