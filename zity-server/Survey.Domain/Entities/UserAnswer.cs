using Survey.Domain.Core.Models;

namespace Survey.Domain.Entities;
public partial class UserAnswer : BaseEntity
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? AnswerId { get; set; }

    public int? UserId { get; set; }

    public virtual Answer? Answer { get; set; }
}
