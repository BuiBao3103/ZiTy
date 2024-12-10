namespace Billing.Application.DTOs.Momo;

public class MomoCreatePaymentDto
{
    public string RequestId { get; set; }
    public int ErrorCode { get; set; }
    public string OrderId { get; set; }
    public string Message { get; set; }
    public string PayUrl { get; set; }
}

