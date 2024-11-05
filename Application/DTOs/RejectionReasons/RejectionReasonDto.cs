
using Application.DTOs.Reports;

namespace Application.DTOs.RejectionReasons;

public class RejectionReasonDTO
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? ReportId { get; set; }

    public ReportDTO? Report { get; set; }
}
