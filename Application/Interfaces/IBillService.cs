using Application.DTOs;
using Application.DTOs.Bills;
using Application.DTOs.Momo;

namespace Application.Interfaces;

public interface IBillService
{
    Task<PaginatedResult<BillDTO>> GetAllAsync(BillQueryDTO queryParam);
    Task<BillDTO> GetByIdAsync(int id, string? includes = null);
    Task<BillDTO> CreateAsync(BillCreateDTO billCreateDTO);
    Task<BillDTO> UpdateAsync(int id, BillUpdateDTO billUpdateDTO);
    Task<BillDTO> PatchAsync(int id, BillPatchDTO billPatchDTO);
    Task DeleteAsync(int id);
    // Task<string> CreatePaymentVNPayAsync(int id);
    // Task<MomoCreatePaymentDto> CreatePaymentMomoAsync(int id, MomoRequestCreatePaymentDto request);
    Task HandleMoMoCallBackAsync(int id, MomoCallBackDto callbackDto);
}
