using Application.DTOs.Momo;
using Domain.Entities;

namespace Application.Core.Services;

public interface IMomoService
{
    Task<MomoCreatePaymentDto> CreatePaymentAsync(Bill bill, MomoRequestCreatePaymentDto request);
}
