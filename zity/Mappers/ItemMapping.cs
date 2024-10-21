using AutoMapper;
using zity.DTOs.Items;
using zity.Models;

namespace zity.Mappers
{
    public class ItemMapping : Profile
    {
        public ItemMapping()
        {
            CreateMap<Item, ItemDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<ItemCreateDTO, Item>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ItemUpdateDTO, Item>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ItemPatchDTO, Item>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
