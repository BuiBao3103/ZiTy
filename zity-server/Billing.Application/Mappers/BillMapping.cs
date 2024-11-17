using AutoMapper;
using Billing.Domain.Entities;
using Billing.Application.DTOs.Bills;

namespace Billing.Application.Mappers;

public class BillMapping : Profile
{
    public BillMapping()
    {
        CreateMap<Bill, BillDTO>()
         .ForMember(dest => dest.BillDetails, opt => opt.MapFrom(src => src.BillDetails));

        CreateMap<BillCreateDTO, Bill>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<BillUpdateDTO, Bill>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<BillPatchDTO, Bill>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Monthly, opt => opt.Condition((src, dest) => src.Monthly != null))
            .ForMember(dest => dest.TotalPrice, opt => opt.Condition((src, dest) => src.TotalPrice != null))
            .ForMember(dest => dest.OldWater, opt => opt.Condition((src, dest) => src.OldWater != null))
            .ForMember(dest => dest.NewWater, opt => opt.Condition((src, dest) => src.NewWater != null))
            .ForMember(dest => dest.WaterReadingDate, opt => opt.Condition((src, dest) => src.WaterReadingDate != null))
            .ForMember(dest => dest.Status, opt => opt.Condition((src, dest) => src.Status != null))
            .ForMember(dest => dest.RelationshipId, opt => opt.Condition((src, dest) => src.RelationshipId != null));
    }
}

