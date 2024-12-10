using Application.DTOs.Reports;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        CreateMap<Report, ReportDTO>()
            .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.RejectionReason))
            .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => src.Relationship));

        CreateMap<ReportCreateDTO, Report>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<ReportUpdateDTO, Report>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<ReportPatchDTO, Report>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Content, opt => opt.Condition((src, dest) => src.Content != null))
            .ForMember(dest => dest.Title, opt => opt.Condition((src, dest) => src.Title != null))
            .ForMember(dest => dest.Status, opt => opt.Condition((src, dest) => src.Status != null))
            .ForMember(dest => dest.RelationshipId, opt => opt.Condition((src, dest) => src.RelationshipId != null));
    }
}

