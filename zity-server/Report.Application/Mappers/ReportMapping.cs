using AutoMapper;
using Report.Application.DTOs.Reports;

namespace Report.Application.Mappers;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        IMappingExpression<Report.Domain.Entities.Report, ReportDTO> mappingExpression = CreateMap<Report.Domain.Entities.Report, ReportDTO>()
            .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.RejectionReason));

        CreateMap<ReportCreateDTO, Report.Domain.Entities.Report>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<ReportUpdateDTO, Report.Domain.Entities.Report>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<ReportPatchDTO, Report.Domain.Entities.Report>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Content, opt => opt.Condition((src, dest) => src.Content != null))
            .ForMember(dest => dest.Title, opt => opt.Condition((src, dest) => src.Title != null))
            .ForMember(dest => dest.Status, opt => opt.Condition((src, dest) => src.Status != null))
            .ForMember(dest => dest.RelationshipId, opt => opt.Condition((src, dest) => src.RelationshipId != null));
    }
}

