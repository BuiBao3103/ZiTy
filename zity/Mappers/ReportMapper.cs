
using zity.DTOs.Reports;
using zity.Models;

namespace zity.Mappers
{
    public class ReportMapper
    {
        // ToDTO
        public static ReportDTO ReportToDTO(Report report) =>
            new ReportDTO
            {
                Id = report.Id,
                Title = report.Title,
                Content = report.Content,
                Status = report.Status,
                RelationshipId = report.RelationshipId
            };

        // ToModelFromCreate
        public static Report ToModelFromCreate(ReportCreateDTO report) =>
            new Report
            {
                Title = report.Title,
                Content = report.Content,
                Status = report.Status,
                RelationshipId = report.RelationshipId,
                CreatedAt = DateTime.Now
            };

        // UpdateModelFromUpdate
        public static Report UpdateModelFromUpdate(Report report, ReportUpdateDTO reportUpdateDTO)
        {
            report.Title = reportUpdateDTO.Title;
            report.Content = reportUpdateDTO.Content;
            report.Status = reportUpdateDTO.Status;
            report.RelationshipId = reportUpdateDTO.RelationshipId;
            report.UpdatedAt = DateTime.Now;

            return report;
        }

        // PatchModelFromPatch
        public static Report PatchModelFromPatch(Report report, ReportPatchDTO reportPatchDTO)
        {
            if (reportPatchDTO.Title != null)
            {
                report.Title = reportPatchDTO.Title;
            }

            if (reportPatchDTO.Content != null)
            {
                report.Content = reportPatchDTO.Content;
            }

            if (reportPatchDTO.Status != null)
            {
                report.Status = reportPatchDTO.Status;
            }

            if (reportPatchDTO.RelationshipId != null)
            {
                report.RelationshipId = reportPatchDTO.RelationshipId.Value;
            }
            report.UpdatedAt = DateTime.Now;

            return report;
        }

    }
}
