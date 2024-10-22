using System.Threading.Tasks;
using zity.DTOs.OtherAnswers;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class OtherAnswerService(IOtherAnswerRepository otherAnswerRepository) : IOtherAnswerService
    {
        private readonly IOtherAnswerRepository _otherAnswerRepository = otherAnswerRepository;

        public async Task<PaginatedResult<OtherAnswerDTO>> GetAllAsync(OtherAnswerQueryDTO queryParam)
        {
            var pageOtherAnswers = await _otherAnswerRepository.GetAllAsync(queryParam);

            return new PaginatedResult<OtherAnswerDTO>(
                pageOtherAnswers.Contents.Select(OtherAnswerMapper.ToDTO).ToList(),
                pageOtherAnswers.TotalItems,
                pageOtherAnswers.Page,
                pageOtherAnswers.PageSize);
        }


        public async Task<OtherAnswerDTO?> GetByIdAsync(int id, string? includes)
        {
            var otherAnswer = await _otherAnswerRepository.GetByIdAsync(id, includes);
            return otherAnswer != null ? OtherAnswerMapper.ToDTO(otherAnswer) : null;
        }
        public async Task<OtherAnswerDTO> CreateAsync(OtherAnswerCreateDTO createDTO)
        {
            var otherAnswer = OtherAnswerMapper.ToModelFromCreate(createDTO);
            return OtherAnswerMapper.ToDTO(await _otherAnswerRepository.CreateAsync(otherAnswer));
        }
        public async Task<OtherAnswerDTO?> UpdateAsync(int id, OtherAnswerUpdateDTO updateDTO)
        {
            var existingOtherAnswer = await _otherAnswerRepository.GetByIdAsync(id, null);
            if (existingOtherAnswer == null)
            {
                return null;
            }

            OtherAnswerMapper.UpdateModelFromUpdate(existingOtherAnswer, updateDTO);
            var updatedOtherAnswer = await _otherAnswerRepository.UpdateAsync(existingOtherAnswer);
            return OtherAnswerMapper.ToDTO(updatedOtherAnswer);
        }

        public async Task<OtherAnswerDTO?> PatchAsync(int id, OtherAnswerPatchDTO patchDTO)
        {
            var existingOtherAnswer = await _otherAnswerRepository.GetByIdAsync(id, null);
            if (existingOtherAnswer == null)
            {
                return null;
            }

            OtherAnswerMapper.PatchModelFromPatch(existingOtherAnswer, patchDTO);
            var patchedOtherAnswer = await _otherAnswerRepository.UpdateAsync(existingOtherAnswer);
            return OtherAnswerMapper.ToDTO(patchedOtherAnswer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _otherAnswerRepository.DeleteAsync(id);
        }

    }
}
