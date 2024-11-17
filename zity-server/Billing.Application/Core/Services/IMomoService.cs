using Billing.Application.DTOs.Momo;
using Billing.Domain.Entities;

namespace Billing.Application.Core.Services;

public interface IMomoService
{
    Task<MomoCreatePaymentDto> CreatePaymentAsync(Bill bill, MomoRequestCreatePaymentDto request);
}
