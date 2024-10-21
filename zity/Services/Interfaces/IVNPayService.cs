using zity.Models;

namespace zity.Services.Interfaces
{
    public interface IVNPayService
    {
        public string CreatePaymentUrl(Bill bill);
    }
}
