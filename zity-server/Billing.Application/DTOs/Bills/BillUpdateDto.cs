using System.ComponentModel.DataAnnotations;

namespace Billing.Application.DTOs.Bills;

public class BillUpdateDTO
{
    [Required]
    public string Monthly { get; set; } = null!;

    [Required]
    public float? TotalPrice { get; set; }

    public int? OldWater { get; set; }

    public int? NewWater { get; set; }

    public DateTime? WaterReadingDate { get; set; }

    [Required]
    public string Status { get; set; } = null!;

    [Required]
    public int? RelationshipId { get; set; }
}

