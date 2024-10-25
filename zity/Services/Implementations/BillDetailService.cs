using AutoMapper;
using System.Threading.Tasks;
using zity.DTOs.BillDetails;
using zity.ExceptionHandling.Exceptions;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class BillDetailService(IBillDetailRepository billDetailRepository, IMapper mapper) : IBillDetailService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IBillDetailRepository _billDetailRepository = billDetailRepository;

        public async Task<PaginatedResult<BillDetailDTO>> GetAllAsync(BillDetailQueryDTO queryParam)
        {
            var pageBillDetails = await _billDetailRepository.GetAllAsync(queryParam);
            var billDetails = pageBillDetails.Contents.Select(_mapper.Map<BillDetailDTO>).ToList();
            return new PaginatedResult<BillDetailDTO>(
                billDetails,
                pageBillDetails.TotalItems,
                pageBillDetails.Page,
                pageBillDetails.PageSize);
        }

        public async Task<BillDetailDTO> GetByIdAsync(int id, string? includes = null)
        {
            var billDetail = await _billDetailRepository.GetByIdAsync(id, includes)
                    ?? throw new EntityNotFoundException(nameof(BillDetail), id);
            return _mapper.Map<BillDetailDTO>(billDetail);
        }

        public async Task<BillDetailDTO> CreateAsync(BillDetailCreateDTO createDTO)
        {
            var billDetail = _mapper.Map<BillDetail>(createDTO);
            return _mapper.Map<BillDetailDTO>(await _billDetailRepository.CreateAsync(billDetail));
        }

        public async Task<BillDetailDTO> UpdateAsync(int id, BillDetailUpdateDTO updateDTO)
        {
            var existingBillDetail = await _billDetailRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(BillDetail), id);
            _mapper.Map(updateDTO, existingBillDetail);
            var updatedBillDetail = await _billDetailRepository.UpdateAsync(existingBillDetail);
            return _mapper.Map<BillDetailDTO>(updatedBillDetail);
        }

        public async Task<BillDetailDTO> PatchAsync(int id, BillDetailPatchDTO patchDTO)
        {
            var existingBillDetail = await _billDetailRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(BillDetail), id);
            _mapper.Map(patchDTO, existingBillDetail);
            var patchedBillDetail = await _billDetailRepository.UpdateAsync(existingBillDetail);
            return _mapper.Map<BillDetailDTO>(patchedBillDetail);
        }

        public async Task DeleteAsync(int id)
        {
            await _billDetailRepository.DeleteAsync(id);
        }
    }
}
