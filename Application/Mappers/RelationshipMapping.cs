using Application.DTOs.Relationships;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class RelationshipMapping : Profile
{
    public RelationshipMapping()
    {
        CreateMap<Relationship, RelationshipDTO>()
         .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
         .ForMember(dest => dest.Apartment, opt => opt.MapFrom(src => src.Apartment))
         .ForMember(dest => dest.Bills, opt => opt.MapFrom(src => src.Bills))
         .ForMember(dest => dest.Reports, opt => opt.MapFrom(src => src.Reports));

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

