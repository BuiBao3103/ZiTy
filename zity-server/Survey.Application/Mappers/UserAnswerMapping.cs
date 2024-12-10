using AutoMapper;
using Survey.Domain.Entities;
using Survey.Application.DTOs.UserAnswers;


namespace Survey.Application.Mappers;

public class UserAnswerMapping : Profile
{
    public UserAnswerMapping()
    {
        CreateMap<UserAnswer, UserAnswerDTO>()
            .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer));

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



