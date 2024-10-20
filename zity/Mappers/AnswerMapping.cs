using AutoMapper;
using zity.DTOs.Answers;
using zity.Models;

namespace zity.Mappers
{
    public class AnswerMapping : Profile
    {
        public AnswerMapping()
        {
            CreateMap<Answer, AnswerDTO>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dest => dest.UserAnswers, opt => opt.MapFrom(src => src.UserAnswers));

            CreateMap<AnswerCreateDTO, Answer>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<AnswerUpdateDTO, Answer>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<AnswerPatchDTO, Answer>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}

