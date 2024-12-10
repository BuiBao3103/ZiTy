using Survey.Domain.Core.Models;

namespace Survey.Domain.Entities;
public partial class OtherAnswer : BaseEntity
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? QuestionId { get; set; }

    public int? UserId { get; set; }

    public virtual Question? Question { get; set; }
}
