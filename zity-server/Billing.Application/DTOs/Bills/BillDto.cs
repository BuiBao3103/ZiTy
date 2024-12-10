using Billing.Application.DTOs.ApartmentService;
using Billing.Application.DTOs.BillDetails;

namespace Billing.Application.DTOs.Bills;

public class BillDTO
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

    public int RelationshipId { get; set; }
    public ICollection<BillDetailDTO> BillDetails { get; set; } = [];

    public RelationshipDTO Relationship { get; set; } = null!;

}

