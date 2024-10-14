using AutoMapper;
using zity.DTOs.Apartments;
using zity.Models;

namespace zity.Mappers
{
    public class ApartmentMapping : Profile
    {
        public ApartmentMapping()
        {
            CreateMap<Apartment, ApartmentDTO>();

            CreateMap<ApartmentCreateDTO, Apartment>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ApartmentUpdateDTO, Apartment>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ApartmentPatchDTO, Apartment>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
