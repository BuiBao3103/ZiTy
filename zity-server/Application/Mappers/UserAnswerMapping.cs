using Application.DTOs.UserAnswers;
using AutoMapper;
using Domain.Entities;


namespace Application.Mappers;

public class UserAnswerMapping : Profile
{
    public UserAnswerMapping()
    {
        CreateMap<UserAnswer, UserAnswerDTO>()
            .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<UserAnswerCreateDTO, UserAnswer>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<UserAnswerUpdateDTO, UserAnswer>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<UserAnswerPatchDTO, UserAnswer>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.AnswerId, opt => opt.Condition((src, dest) => src.AnswerId != null))
            .ForMember(dest => dest.UserId, opt => opt.Condition((src, dest) => src.UserId != null));
    }
}



