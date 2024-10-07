using System.Threading.Tasks;
using zity.DTOs.RejectionReasons;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class RejectionReasonService(IRejectionReasonRepository rejectionReasonRepository) : IRejectionReasonService
    {
        private readonly IRejectionReasonRepository _rejectionReasonRepository = rejectionReasonRepository;

        public async Task<PaginatedResult<RejectionReasonDTO>> GetAllAsync(RejectionReasonQueryDTO queryParam)
        {
            var pageRejectionReasons = await _rejectionReasonRepository.GetAllAsync(queryParam);

            return new PaginatedResult<RejectionReasonDTO>(
                pageRejectionReasons.Contents.Select(RejectionReasonMapper.ToDTO).ToList(),
                pageRejectionReasons.TotalItems,
                pageRejectionReasons.Page,
                pageRejectionReasons.PageSize);
        }


        public async Task<RejectionReasonDTO?> GetByIdAsync(int id, string? includes)
        {
            var rejectionReason = await _rejectionReasonRepository.GetByIdAsync(id, includes);
            return rejectionReason != null ? RejectionReasonMapper.ToDTO(rejectionReason) : null;
        }
        public async Task<RejectionReasonDTO> CreateAsync(RejectionReasonCreateDTO createDTO)
        {
            var rejectionReason = RejectionReasonMapper.ToModelFromCreate(createDTO);
            return RejectionReasonMapper.ToDTO(await _rejectionReasonRepository.CreateAsync(rejectionReason));
        }
        public async Task<RejectionReasonDTO?> UpdateAsync(int id, RejectionReasonUpdateDTO updateDTO)
        {
            var existingRejectionReason = await _rejectionReasonRepository.GetByIdAsync(id, null);
            if (existingRejectionReason == null)
            {
                return null;
            }

            RejectionReasonMapper.UpdateModelFromUpdate(existingRejectionReason, updateDTO);
            var updatedRejectionReason = await _rejectionReasonRepository.UpdateAsync(existingRejectionReason);
            return RejectionReasonMapper.ToDTO(updatedRejectionReason);
        }

        public async Task<RejectionReasonDTO?> PatchAsync(int id, RejectionReasonPatchDTO patchDTO)
        {
            var existingRejectionReason = await _rejectionReasonRepository.GetByIdAsync(id, null);
            if (existingRejectionReason == null)
            {
                return null;
            }

            RejectionReasonMapper.PatchModelFromPatch(existingRejectionReason, patchDTO);
            var patchedRejectionReason = await _rejectionReasonRepository.UpdateAsync(existingRejectionReason);
            return RejectionReasonMapper.ToDTO(patchedRejectionReason);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _rejectionReasonRepository.DeleteAsync(id);
        }

    }
}
