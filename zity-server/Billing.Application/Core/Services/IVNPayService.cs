using Billing.Domain.Entities;

namespace Billing.Application.Core.Services;

public interface IVNPayService
{
    public string CreatePaymentUrl(Bill bill);
}
