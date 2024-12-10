using AutoMapper;
using Billing.Application.DTOs.BillDetails;
using Billing.Domain.Entities;

namespace Billing.Application.Mappers;

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
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Price, opt => opt.Condition((src, dest) => src.Price != null))
            .ForMember(dest => dest.BillId, opt => opt.Condition((src, dest) => src.BillId != null))
            .ForMember(dest => dest.ServiceId, opt => opt.Condition((src, dest) => src.ServiceId != null));
    }
}

