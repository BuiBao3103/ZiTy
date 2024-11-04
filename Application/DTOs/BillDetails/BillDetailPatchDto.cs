namespace Application.DTOs.BillDetails;

public class BillDetailPatchDTO
{
    public float? Price { get; set; }

    public int? BillId { get; set; }

    public int? ServiceId { get; set; }
}

