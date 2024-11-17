using System.ComponentModel.DataAnnotations;

namespace Billing.Application.DTOs.BillDetails;

public class BillDetailCreateDTO
{
    [Required]
    public float? Price { get; set; }

    [Required]
    public int? BillId { get; set; }

    [Required]
    public int? ServiceId { get; set; }
}


