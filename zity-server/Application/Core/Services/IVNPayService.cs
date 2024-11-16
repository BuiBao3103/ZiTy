using Domain.Entities;

namespace Application.Core.Services;

public interface IVNPayService
{
    public string CreatePaymentUrl(Bill bill);
}
