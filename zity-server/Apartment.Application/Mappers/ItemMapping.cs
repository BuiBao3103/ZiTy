using Apartment.Application.DTOs.Items;
using AutoMapper;
using Apartment.Domain.Entities;

namespace Apartment.Application.Mappers;

public class ItemMapping : Profile
{
    public ItemMapping()
    {
        CreateMap<Item, ItemDTO>();

        CreateMap<ItemCreateDTO, Item>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.IsReceive, opt => opt.MapFrom(src => false));

        CreateMap<ItemUpdateDTO, Item>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<ItemPatchDTO, Item>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
            .ForMember(dest => dest.IsReceive, opt => opt.Condition(src => src.IsReceive != null))
            .ForMember(dest => dest.UserId, opt => opt.Condition(src => src.UserId != null));
    }
}
