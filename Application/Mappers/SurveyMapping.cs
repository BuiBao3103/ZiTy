using Application.DTOs.Surveys;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class SurveyMapping : Profile
{
    public SurveyMapping()
    {
        CreateMap<Survey, SurveyDTO>()
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions))
            .ForMember(dest => dest.UserCreate, opt => opt.MapFrom(src => src.UserCreate));

        CreateMap<SurveyCreateDTO, Survey>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<SurveyUpdateDTO, Survey>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<SurveyPatchDTO, Survey>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Title, opt => opt.Condition((src, dest) => src.Title != null))
            .ForMember(dest => dest.StartDate, opt => opt.Condition((src, dest) => src.StartDate != null))
            .ForMember(dest => dest.EndDate, opt => opt.Condition((src, dest) => src.EndDate != null))
            .ForMember(dest => dest.TotalQuestions, opt => opt.Condition((src, dest) => src.TotalQuestions != null))
            .ForMember(dest => dest.UserCreateId, opt => opt.Condition((src, dest) => src.UserCreateId != null));
    }
}

