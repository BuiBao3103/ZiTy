using System.Threading.Tasks;
using zity.DTOs.Answers;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class AnswerService(IAnswerRepository answerRepository) : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository = answerRepository;

        public async Task<PaginatedResult<AnswerDTO>> GetAllAsync(AnswerQueryDTO queryParam)
        {
            var pageAnswers = await _answerRepository.GetAllAsync(queryParam);

            return new PaginatedResult<AnswerDTO>(
                pageAnswers.Contents.Select(AnswerMapper.ToDTO).ToList(),
                pageAnswers.TotalItems,
                pageAnswers.Page,
                pageAnswers.PageSize);
        }


        public async Task<AnswerDTO?> GetByIdAsync(int id, string? includes)
        {
            var answer = await _answerRepository.GetByIdAsync(id, includes);
            return answer != null ? AnswerMapper.ToDTO(answer) : null;
        }
        public async Task<AnswerDTO> CreateAsync(AnswerCreateDTO createDTO)
        {
            var answer = AnswerMapper.ToModelFromCreate(createDTO);
            return AnswerMapper.ToDTO(await _answerRepository.CreateAsync(answer));
        }
        public async Task<AnswerDTO?> UpdateAsync(int id, AnswerUpdateDTO updateDTO)
        {
            var existingAnswer = await _answerRepository.GetByIdAsync(id, null);
            if (existingAnswer == null)
            {
                return null;
            }

            AnswerMapper.UpdateModelFromUpdate(existingAnswer, updateDTO);
            var updatedAnswer = await _answerRepository.UpdateAsync(existingAnswer);
            return AnswerMapper.ToDTO(updatedAnswer);
        }

        public async Task<AnswerDTO?> PatchAsync(int id, AnswerPatchDTO patchDTO)
        {
            var existingAnswer = await _answerRepository.GetByIdAsync(id, null);
            if (existingAnswer == null)
            {
                return null;
            }

            AnswerMapper.PatchModelFromPatch(existingAnswer, patchDTO);
            var patchedAnswer = await _answerRepository.UpdateAsync(existingAnswer);
            return AnswerMapper.ToDTO(patchedAnswer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _answerRepository.DeleteAsync(id);
        }

    }
}
