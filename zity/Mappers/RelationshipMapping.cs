using AutoMapper;
using zity.DTOs.Relationships;
using zity.Models;

namespace zity.Mappers
{
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
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
