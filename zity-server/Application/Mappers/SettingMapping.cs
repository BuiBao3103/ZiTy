using Application.DTOs.Settings;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class SettingMapping : Profile
{
    public SettingMapping()
    {
        CreateMap<Setting, SettingDTO>();

        CreateMap<SettingPatchDTO, Setting>()
         .ForMember(dest => dest.CurrentMonthly, opt => opt.Condition((src, dest) => src.CurrentMonthly != null))
         .ForMember(dest => dest.SystemStatus, opt => opt.Condition((src, dest) => src.SystemStatus != null))
         .ForMember(dest => dest.RoomPricePerM2, opt => opt.Condition((src, dest) => src.RoomPricePerM2 != null))
         .ForMember(dest => dest.WaterPricePerM3, opt => opt.Condition((src, dest) => src.WaterPricePerM3 != null))
         .ForMember(dest => dest.RoomVat, opt => opt.Condition((src, dest) => src.RoomVat != null))
         .ForMember(dest => dest.WaterVat, opt => opt.Condition((src, dest) => src.WaterVat != null))
         .ForMember(dest => dest.EnvProtectionTax, opt => opt.Condition((src, dest) => src.EnvProtectionTax != null));
    }
}
