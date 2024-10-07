using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.RejectionReasons
{
    public class RejectionReasonUpdateDTO
    {
        [Required]
        public string Content { get; set; } = null!;
        [Required]
        public int ReportId { get; set; }
    }
}
