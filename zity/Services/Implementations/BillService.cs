using AutoMapper;
using zity.DTOs.Bills;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class BillService(IBillRepository billRepository, IMapper mapper) : IBillService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IBillRepository _billRepository = billRepository;

        public async Task<PaginatedResult<BillDTO>> GetAllAsync(BillQueryDTO queryParam)
        {
            var pageBills = await _billRepository.GetAllAsync(queryParam);

            var bills = pageBills.Contents.Select(_mapper.Map<BillDTO>).ToList();
            return new PaginatedResult<BillDTO>(
                bills,
                pageBills.TotalItems,
                pageBills.Page,
                pageBills.PageSize);
        }

        public async Task<BillDTO?> GetByIdAsync(int id, string? includes)
        {
            var bill = await _billRepository.GetByIdAsync(id, includes);
            return bill != null ? _mapper.Map<BillDTO>(bill) : null;
        }

        public async Task<BillDTO> CreateAsync(BillCreateDTO createDTO)
        {
            var bill = _mapper.Map<Bill>(createDTO);
            return _mapper.Map<BillDTO>(await _billRepository.CreateAsync(bill));
        }

        public async Task<BillDTO?> UpdateAsync(int id, BillUpdateDTO updateDTO)
        {
            var existingBill = await _billRepository.GetByIdAsync(id, null);
            if (existingBill == null)
            {
                return null;
            }

            _mapper.Map(updateDTO, existingBill);
            var updatedBill = await _billRepository.UpdateAsync(existingBill);
            return _mapper.Map<BillDTO>(updatedBill);
        }

        public async Task<BillDTO?> PatchAsync(int id, BillPatchDTO patchDTO)
        {
            var existingBill = await _billRepository.GetByIdAsync(id, null);
            if (existingBill == null)
            {
                return null;
            }

            _mapper.Map(patchDTO, existingBill);
            var patchedBill = await _billRepository.UpdateAsync(existingBill);
            return _mapper.Map<BillDTO>(patchedBill);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _billRepository.DeleteAsync(id);
        }

    }
}
