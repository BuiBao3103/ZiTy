using Domain.Core.Models;

namespace Domain.Entities;

public partial class Bill : BaseEntity
{
    public int Id { get; set; }

    public string Monthly { get; set; } = null!;

    public float TotalPrice { get; set; }

    public int? OldWater { get; set; }

    public int? NewWater { get; set; }

    public DateTime? WaterReadingDate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int RelationshipId { get; set; }

    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();
}
