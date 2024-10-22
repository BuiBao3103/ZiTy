using AutoMapper;
using System.Threading.Tasks;
using zity.DTOs.UserAnswers;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class UserAnswerService(IUserAnswerRepository userAnswerRepository, IMapper mapper) : IUserAnswerService
    {
        private readonly IUserAnswerRepository _userAnswerRepository = userAnswerRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<PaginatedResult<UserAnswerDTO>> GetAllAsync(UserAnswerQueryDTO queryParam)
        {
            var pageUserAnswers = await _userAnswerRepository.GetAllAsync(queryParam);
            var userAnswers = pageUserAnswers.Contents.Select(_mapper.Map<UserAnswerDTO>).ToList(); ;
            return new PaginatedResult<UserAnswerDTO>(
                userAnswers,
                pageUserAnswers.TotalItems,
                pageUserAnswers.Page,
                pageUserAnswers.PageSize);
        }


        public async Task<UserAnswerDTO?> GetByIdAsync(int id, string? includes)
        {
            var userAnswer = await _userAnswerRepository.GetByIdAsync(id, includes);
            return userAnswer != null ? _mapper.Map<UserAnswerDTO>(userAnswer) : null;
        }
        public async Task<UserAnswerDTO> CreateAsync(UserAnswerCreateDTO createDTO)
        {
            var userAnswer = _mapper.Map<UserAnswer>(createDTO);
            return _mapper.Map<UserAnswerDTO>(await _userAnswerRepository.CreateAsync(userAnswer));
        }
        public async Task<UserAnswerDTO?> UpdateAsync(int id, UserAnswerUpdateDTO updateDTO)
        {
            var existingUserAnswer = await _userAnswerRepository.GetByIdAsync(id, null);
            if (existingUserAnswer == null)
            {
                return null;
            }

            _mapper.Map(updateDTO, existingUserAnswer);
            var updatedUserAnswer = await _userAnswerRepository.UpdateAsync(existingUserAnswer);
            return _mapper.Map<UserAnswerDTO>(updatedUserAnswer);
        }

        public async Task<UserAnswerDTO?> PatchAsync(int id, UserAnswerPatchDTO patchDTO)
        {
            var existingUserAnswer = await _userAnswerRepository.GetByIdAsync(id, null);
            if (existingUserAnswer == null)
            {
                return null;
            }

            _mapper.Map(patchDTO, existingUserAnswer);
            var patchedUserAnswer = await _userAnswerRepository.UpdateAsync(existingUserAnswer);
            return _mapper.Map<UserAnswerDTO>(patchedUserAnswer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userAnswerRepository.DeleteAsync(id);
        }

    }
}
