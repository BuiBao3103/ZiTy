using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.RejectionReasons;

public class RejectionReasonUpdateDTO
{
    [Required]
    public string Content { get; set; } = null!;
    [Required]
    public int? ReportId { get; set; }
}

