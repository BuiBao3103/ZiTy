using zity.DTOs.Momo;
using zity.Models;

namespace zity.Services.Interfaces
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentDto> CreatePaymentAsync(Bill bill);
    }
}
