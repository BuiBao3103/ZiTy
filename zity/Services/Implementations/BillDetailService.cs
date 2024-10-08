using System.Threading.Tasks;
using zity.DTOs.BillDetails;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class BillDetailService(IBillDetailRepository billDetailRepository) : IBillDetailService
    {
        private readonly IBillDetailRepository _billDetailRepository = billDetailRepository;

        public async Task<PaginatedResult<BillDetailDTO>> GetAllAsync(BillDetailQueryDTO queryParam)
        {
            var pageBillDetails = await _billDetailRepository.GetAllAsync(queryParam);

            return new PaginatedResult<BillDetailDTO>(
                pageBillDetails.Contents.Select(BillDetailMapper.ToDTO).ToList(),
                pageBillDetails.TotalItems,
                pageBillDetails.Page,
                pageBillDetails.PageSize);
        }

        public async Task<BillDetailDTO?> GetByIdAsync(int id, string? includes)
        {
            var billDetail = await _billDetailRepository.GetByIdAsync(id, includes);
            return billDetail != null ? BillDetailMapper.ToDTO(billDetail) : null;
        }

        public async Task<BillDetailDTO> CreateAsync(BillDetailCreateDTO createDTO)
        {
            var billDetail = BillDetailMapper.ToModelFromCreate(createDTO);
            return BillDetailMapper.ToDTO(await _billDetailRepository.CreateAsync(billDetail));
        }

        public async Task<BillDetailDTO?> UpdateAsync(int id, BillDetailUpdateDTO updateDTO)
        {
            var existingBillDetail = await _billDetailRepository.GetByIdAsync(id, null);
            if (existingBillDetail == null)
            {
                return null;
            }

            BillDetailMapper.UpdateModelFromUpdate(existingBillDetail, updateDTO);
            var updatedBillDetail = await _billDetailRepository.UpdateAsync(existingBillDetail);
            return BillDetailMapper.ToDTO(updatedBillDetail);
        }

        public async Task<BillDetailDTO?> PatchAsync(int id, BillDetailPatchDTO patchDTO)
        {
            var existingBillDetail = await _billDetailRepository.GetByIdAsync(id, null);
            if (existingBillDetail == null)
            {
                return null;
            }

            BillDetailMapper.PatchModelFromPatch(existingBillDetail, patchDTO);
            var patchedBillDetail = await _billDetailRepository.UpdateAsync(existingBillDetail);
            return BillDetailMapper.ToDTO(patchedBillDetail);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _billDetailRepository.DeleteAsync(id);
        }
    }
}
