using zity.DTOs.RejectionReasons;
using zity.DTOs.Relationships;

namespace zity.DTOs.Reports
{
    public class ReportDTO
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? RelationshipId { get; set; }

        public ICollection<RejectionReasonDTO> RejectionReasons { get; set; } = [];

        public virtual RelationshipDTO? Relationship { get; set; }
    }
}
