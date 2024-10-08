using zity.Models;

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

        public ICollection<RejectionReason> RejectionReasons { get; set; } = new List<RejectionReason>();

        public Relationship? Relationship { get; set; }
    }
}
