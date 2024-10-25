using AutoMapper;
using System.Threading.Tasks;
using zity.DTOs.UserAnswers;
using zity.ExceptionHandling.Exceptions;
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
        public async Task<UserAnswerDTO> GetByIdAsync(int id, string? includes = null)
        {
            var userAnswer = await _userAnswerRepository.GetByIdAsync(id, includes)
                    ?? throw new EntityNotFoundException(nameof(UserAnswer), id);
            return _mapper.Map<UserAnswerDTO>(userAnswer);
        }
        public async Task<UserAnswerDTO> CreateAsync(UserAnswerCreateDTO createDTO)
        {
            var userAnswer = _mapper.Map<UserAnswer>(createDTO);
            return _mapper.Map<UserAnswerDTO>(await _userAnswerRepository.CreateAsync(userAnswer));
        }
        public async Task<UserAnswerDTO> UpdateAsync(int id, UserAnswerUpdateDTO updateDTO)
        {
            var existingUserAnswer = await _userAnswerRepository.GetByIdAsync(id, null)
                    ?? throw new EntityNotFoundException(nameof(UserAnswer), id);
            _mapper.Map(updateDTO, existingUserAnswer);
            var updatedUserAnswer = await _userAnswerRepository.UpdateAsync(existingUserAnswer);
            return _mapper.Map<UserAnswerDTO>(updatedUserAnswer);
        }

        public async Task<UserAnswerDTO> PatchAsync(int id, UserAnswerPatchDTO patchDTO)
        {
            var existingUserAnswer = await _userAnswerRepository.GetByIdAsync(id, null)
                    ?? throw new EntityNotFoundException(nameof(UserAnswer), id);
            _mapper.Map(patchDTO, existingUserAnswer);
            var patchedUserAnswer = await _userAnswerRepository.UpdateAsync(existingUserAnswer);
            return _mapper.Map<UserAnswerDTO>(patchedUserAnswer);
        }
        public async Task DeleteAsync(int id)
        {
            await _userAnswerRepository.DeleteAsync(id);
        }

    }
}
