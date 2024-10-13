using AutoMapper;
using zity.DTOs.Bills;
using zity.Models;

namespace zity.Mappers
{
    public class BillMapping : Profile
    {
        public BillMapping()
        {
            CreateMap<Bill, BillDTO>()
                .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => src.Relationship))
                .ForMember(dest => dest.BillDetails, opt => opt.MapFrom(src => src.BillDetails)); 

            CreateMap<BillCreateDTO, Bill>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<BillUpdateDTO, Bill>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<BillPatchDTO, Bill>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
