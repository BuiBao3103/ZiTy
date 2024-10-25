using AutoMapper;
using System.Threading.Tasks;
using zity.DTOs.RejectionReasons;
using zity.ExceptionHandling.Exceptions;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class RejectionReasonService(IRejectionReasonRepository rejectionReasonRepository, IMapper mapper) : IRejectionReasonService
    {
        private readonly IRejectionReasonRepository _rejectionReasonRepository = rejectionReasonRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<PaginatedResult<RejectionReasonDTO>> GetAllAsync(RejectionReasonQueryDTO queryParam)
        {
            var pageRejectionReasons = await _rejectionReasonRepository.GetAllAsync(queryParam);
            var rejectionReasons = pageRejectionReasons.Contents.Select(_mapper.Map<RejectionReasonDTO>).ToList();
            return new PaginatedResult<RejectionReasonDTO>(
                rejectionReasons,
                pageRejectionReasons.TotalItems,
                pageRejectionReasons.Page,
                pageRejectionReasons.PageSize);
        }


        public async Task<RejectionReasonDTO> GetByIdAsync(int id, string? includes)
        {
            var rejectionReason = await _rejectionReasonRepository.GetByIdAsync(id, includes)
                    ?? throw new EntityNotFoundException(nameof(RejectionReason), id);
            return _mapper.Map<RejectionReasonDTO> (rejectionReason);
        }
        public async Task<RejectionReasonDTO> CreateAsync(RejectionReasonCreateDTO createDTO)
        {
            var rejectionReason = _mapper.Map<RejectionReason>(createDTO);
            return _mapper.Map<RejectionReasonDTO>(await _rejectionReasonRepository.CreateAsync(rejectionReason));
        }
        public async Task<RejectionReasonDTO> UpdateAsync(int id, RejectionReasonUpdateDTO updateDTO)
        {
            var existingRejectionReason = await _rejectionReasonRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(RejectionReason), id);
            _mapper.Map(updateDTO, existingRejectionReason);
            var updatedRejectionReason = await _rejectionReasonRepository.UpdateAsync(existingRejectionReason);
            return _mapper.Map<RejectionReasonDTO>(updatedRejectionReason);
        }

        public async Task<RejectionReasonDTO> PatchAsync(int id, RejectionReasonPatchDTO patchDTO)
        {
            var existingRejectionReason = await _rejectionReasonRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(RejectionReason), id);
            _mapper.Map(patchDTO, existingRejectionReason);
            var patchedRejectionReason = await _rejectionReasonRepository.UpdateAsync(existingRejectionReason);
            return _mapper.Map<RejectionReasonDTO>(patchedRejectionReason);
        }

        public async Task DeleteAsync(int id)
        {
            await _rejectionReasonRepository.DeleteAsync(id);
        }

    }
}
