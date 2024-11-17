namespace Billing.Application.DTOs.Momo;

public class MomoCallBackDto
{
    public string OrderType { get; set; } = null!;
    public decimal Amount { get; set; }
    public string PartnerCode { get; set; } = null!;
    public string OrderId { get; set; } = null!;
    public string ExtraData { get; set; } = null!;
    public string Signature { get; set; } = null!;
    public long TransId { get; set; }
    public long ResponseTime { get; set; }
    public int ResultCode { get; set; }
    public string Message { get; set; } = null!;
    public string PayType { get; set; } = null!;
    public string RequestId { get; set; } = null!;
    public string OrderInfo { get; set; } = null!;
}
