using System.ComponentModel.DataAnnotations;

namespace zity.DTOs.Reports
{
    public class ReportCreateDTO
    {
        [Required]
        public string Content { get; set; } = null!;
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Status { get; set; } = null!;
        [Required]
        public int RelationshipId { get; set; }
    }
}
