using zity.DTOs.Bills;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class BillService(IBillRepository billRepository) : IBillService
    {
        private readonly IBillRepository _billRepository = billRepository;

        public async Task<PaginatedResult<BillDTO>> GetAllAsync(BillQueryDTO queryParam)
        {
            var pageBills = await _billRepository.GetAllAsync(queryParam);

            return new PaginatedResult<BillDTO>(
                pageBills.Contents.Select(BillMapper.ToDTO).ToList(),
                pageBills.TotalItems,
                pageBills.Page,
                pageBills.PageSize);
        }

        public async Task<BillDTO?> GetByIdAsync(int id, string? includes)
        {
            var bill = await _billRepository.GetByIdAsync(id, includes);
            return bill != null ? BillMapper.ToDTO(bill) : null;
        }

        public async Task<BillDTO> CreateAsync(BillCreateDTO createDTO)
        {
            var bill = BillMapper.ToModelFromCreate(createDTO);
            return BillMapper.ToDTO(await _billRepository.CreateAsync(bill));
        }

        public async Task<BillDTO?> UpdateAsync(int id, BillUpdateDTO updateDTO)
        {
            var existingBill = await _billRepository.GetByIdAsync(id, null);
            if (existingBill == null)
            {
                return null;
            }

            BillMapper.UpdateModelFromUpdate(existingBill, updateDTO);
            var updatedBill = await _billRepository.UpdateAsync(existingBill);
            return BillMapper.ToDTO(updatedBill);
        }

        public async Task<BillDTO?> PatchAsync(int id, BillPatchDTO patchDTO)
        {
            var existingBill = await _billRepository.GetByIdAsync(id, null);
            if (existingBill == null)
            {
                return null;
            }

            BillMapper.PatchModelFromPatch(existingBill, patchDTO);
            var patchedBill = await _billRepository.UpdateAsync(existingBill);
            return BillMapper.ToDTO(patchedBill);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _billRepository.DeleteAsync(id);
        }

    }
}
