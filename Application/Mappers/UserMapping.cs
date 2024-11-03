using Application.DTOs.Users;
using AutoMapper;
using Domain.Entities;


namespace Application.Mappers;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDTO>();
            //.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            //.ForMember(dest => dest.OtherAnswers, opt => opt.MapFrom(src => src.OtherAnswers))
            //.ForMember(dest => dest.Relationships, opt => opt.MapFrom(src => src.Relationships))
            //.ForMember(dest => dest.Surveys, opt => opt.MapFrom(src => src.Surveys))
            //.ForMember(dest => dest.UserAnswers, opt => opt.MapFrom(src => src.UserAnswers));

        CreateMap<UserCreateDTO, User>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.IsFirstLogin, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.IsStaying, opt => opt.MapFrom(src => true));

        CreateMap<UserUpdateDTO, User>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<UserPatchDTO, User>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
    }
}


