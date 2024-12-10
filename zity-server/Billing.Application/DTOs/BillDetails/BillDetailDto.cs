using Billing.Application.DTOs.Bills;
// using Application.DTOs.Services;
namespace Billing.Application.DTOs.BillDetails;

public class BillDetailDTO
{
    public int Id { get; set; }

    public float Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int BillId { get; set; }

    public int ServiceId { get; set; }

    public BillDTO? Bill { get; set; } = null;

    // public ServiceDTO? Service { get; set; } = null;
}

