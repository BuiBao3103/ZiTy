using AutoMapper;
using Identity.Domain.Entities;
using Identity.Application.DTOs.Users;


namespace Identity.Application.Mappers;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDTO>();

        CreateMap<UserCreateDTO, User>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.IsFirstLogin, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.IsStaying, opt => opt.MapFrom(src => true));

        CreateMap<UserUpdateDTO, User>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<UserPatchDTO, User>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.IsStaying, opt => opt.Condition((src, dest) => src.IsStaying != null))
            .ForMember(dest => dest.Email, opt => opt.Condition((src, dest) => src.Email != null))
            .ForMember(dest => dest.Phone, opt => opt.Condition((src, dest) => src.Phone != null))
            .ForMember(dest => dest.DateOfBirth, opt => opt.Condition((src, dest) => src.DateOfBirth != null))
            .ForMember(dest => dest.FullName, opt => opt.Condition((src, dest) => src.FullName != null))
            .ForMember(dest => dest.Gender, opt => opt.Condition((src, dest) => src.Gender != null))
            .ForMember(dest => dest.NationId, opt => opt.Condition((src, dest) => src.NationId != null));


    }
}