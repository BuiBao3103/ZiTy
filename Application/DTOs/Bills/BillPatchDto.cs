namespace Application.DTOs.Bills;

public class BillPatchDTO
{
    public string? Monthly { get; set; }

    public float? TotalPrice { get; set; }

    public int? OldWater { get; set; }

    public int? NewWater { get; set; }

    public DateTime? WaterReadingDate { get; set; }

    public string? Status { get; set; }

    public int? RelationshipId { get; set; }
}
