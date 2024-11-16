using Survey.Domain.Core.Models;

namespace Survey.Domain.Entities;
public partial class Survey : BaseEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int TotalQuestions { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? UserCreateId { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
