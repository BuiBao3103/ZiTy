using Application.DTOs.RejectionReasons;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class RejectionReasonMapping : Profile
{
    public RejectionReasonMapping()
    {
        CreateMap<RejectionReason, RejectionReasonDTO>()
            .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.Report));

        CreateMap<RejectionReasonCreateDTO, RejectionReason>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<RejectionReasonUpdateDTO, RejectionReason>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<RejectionReasonPatchDTO, RejectionReason>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Content, opt => opt.Condition((src, dest) => src.Content != null))
            .ForMember(dest => dest.ReportId, opt => opt.Condition((src, dest) => src.ReportId != null));
    }
}

