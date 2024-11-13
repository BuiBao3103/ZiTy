using Report.Domain.Core.Models;

namespace Report.Domain.Entities;
public partial class RejectionReason : BaseEntity
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? ReportId { get; set; }

    public virtual Report? Report { get; set; }
}
