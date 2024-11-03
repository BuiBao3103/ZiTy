using Application.DTOs.Items;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class ItemMapping : Profile
    {
        public ItemMapping()
        {
            CreateMap<Item, ItemDTO>();
            // .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<ItemCreateDTO, Item>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ItemUpdateDTO, Item>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ItemPatchDTO, Item>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Image, opt => opt.Condition(src => src.Image != null))
                .ForMember(dest => dest.Description, opt => opt.Condition(src => src.Description != null))
                .ForMember(dest => dest.IsReceive, opt => opt.Condition(src => src.IsReceive != null))
                .ForMember(dest => dest.UserId, opt => opt.Condition(src => src.UserId != null));
        }
    }
}
