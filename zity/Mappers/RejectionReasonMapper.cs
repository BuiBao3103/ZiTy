using zity.DTOs.RejectionReasons;
using zity.Models;
using zity.Utilities;

namespace zity.Mappers
{
    public class RejectionReasonMapper
    {
        public static RejectionReasonDTO ToDTO(RejectionReason rejectionReason)
        {
            return new RejectionReasonDTO
            {
                Id = rejectionReason.Id,
                CreatedAt = rejectionReason.CreatedAt,
                UpdatedAt = rejectionReason.UpdatedAt,
                Content = rejectionReason.Content,
                ReportId = rejectionReason.ReportId,
                // Report = rejectionReason.Report != null ? ReportMapper.ToDTO(rejectionReason.Report) : null,
            };
        }

        public static RejectionReason ToModelFromCreate(RejectionReasonCreateDTO rejectionReasonCreateDTO)
        {
            return new RejectionReason
            {
                Content = rejectionReasonCreateDTO.Content,
                ReportId = rejectionReasonCreateDTO.ReportId,
                CreatedAt = DateTime.Now,
            };
        }

        public static RejectionReason UpdateModelFromUpdate(RejectionReason rejectionReason, RejectionReasonUpdateDTO updateDTO)
        {
            rejectionReason.Content = updateDTO.Content;
            rejectionReason.ReportId = updateDTO.ReportId;
            rejectionReason.UpdatedAt = DateTime.Now;
            return rejectionReason;
        }

        public static RejectionReason PatchModelFromPatch(RejectionReason rejectionReason, RejectionReasonPatchDTO patchDTO)
        {
            if (patchDTO.Content != null)
                rejectionReason.Content = patchDTO.Content;
            if (patchDTO.ReportId != null)
                rejectionReason.ReportId = patchDTO.ReportId.Value;
            rejectionReason.UpdatedAt = DateTime.Now;
            return rejectionReason;
        }
    }
}
