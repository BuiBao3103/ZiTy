using System.Threading.Tasks;
using zity.DTOs.UserAnswers;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class UserAnswerService(IUserAnswerRepository userAnswerRepository) : IUserAnswerService
    {
        private readonly IUserAnswerRepository _userAnswerRepository = userAnswerRepository;

        public async Task<PaginatedResult<UserAnswerDTO>> GetAllAsync(UserAnswerQueryDTO queryParam)
        {
            var pageUserAnswers = await _userAnswerRepository.GetAllAsync(queryParam);

            return new PaginatedResult<UserAnswerDTO>(
                pageUserAnswers.Contents.Select(UserAnswerMapper.ToDTO).ToList(),
                pageUserAnswers.TotalItems,
                pageUserAnswers.Page,
                pageUserAnswers.PageSize);
        }


        public async Task<UserAnswerDTO?> GetByIdAsync(int id, string? includes)
        {
            var userAnswer = await _userAnswerRepository.GetByIdAsync(id, includes);
            return userAnswer != null ? UserAnswerMapper.ToDTO(userAnswer) : null;
        }
        public async Task<UserAnswerDTO> CreateAsync(UserAnswerCreateDTO createDTO)
        {
            var userAnswer = UserAnswerMapper.ToModelFromCreate(createDTO);
            return UserAnswerMapper.ToDTO(await _userAnswerRepository.CreateAsync(userAnswer));
        }
        public async Task<UserAnswerDTO?> UpdateAsync(int id, UserAnswerUpdateDTO updateDTO)
        {
            var existingUserAnswer = await _userAnswerRepository.GetByIdAsync(id, null);
            if (existingUserAnswer == null)
            {
                return null;
            }

            UserAnswerMapper.UpdateModelFromUpdate(existingUserAnswer, updateDTO);
            var updatedUserAnswer = await _userAnswerRepository.UpdateAsync(existingUserAnswer);
            return UserAnswerMapper.ToDTO(updatedUserAnswer);
        }

        public async Task<UserAnswerDTO?> PatchAsync(int id, UserAnswerPatchDTO patchDTO)
        {
            var existingUserAnswer = await _userAnswerRepository.GetByIdAsync(id, null);
            if (existingUserAnswer == null)
            {
                return null;
            }

            UserAnswerMapper.PatchModelFromPatch(existingUserAnswer, patchDTO);
            var patchedUserAnswer = await _userAnswerRepository.UpdateAsync(existingUserAnswer);
            return UserAnswerMapper.ToDTO(patchedUserAnswer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userAnswerRepository.DeleteAsync(id);
        }

    }
}
