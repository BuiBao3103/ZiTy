using AutoMapper;
using Survey.Domain.Entities;
using Survey.Application.DTOs.OtherAnswers;

namespace Survey.Application.Mappers
{
    public class OtherAnswerMapping : Profile
    {
        public OtherAnswerMapping()
        {
            CreateMap<OtherAnswer, OtherAnswerDTO>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question));

            CreateMap<OtherAnswerCreateDTO, OtherAnswer>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<OtherAnswerUpdateDTO, OtherAnswer>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<OtherAnswerPatchDTO, OtherAnswer>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Content, opt => opt.Condition((src, dest) => src.Content != null))
                .ForMember(dest => dest.QuestionId, opt => opt.Condition((src, dest) => src.QuestionId != null))
                .ForMember(dest => dest.UserId, opt => opt.Condition((src, dest) => src.UserId != null));
        }
    }
}
