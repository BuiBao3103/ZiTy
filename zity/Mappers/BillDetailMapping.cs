using AutoMapper;
using zity.DTOs.BillDetails;
using zity.DTOs.Bills;
using zity.Models;

namespace zity.Mappers
{
    public class BillDetailMapping : Profile
    {
        public BillDetailMapping()
        {
            CreateMap<BillDetail, BillDetailDTO>()
                .ForMember(dest => dest.Bill, opt => opt.MapFrom(src => src.Bill));

            CreateMap<BillDetailCreateDTO, BillDetail>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<BillDetailUpdateDTO, BillDetail>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<BillDetailPatchDTO, BillDetail>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
