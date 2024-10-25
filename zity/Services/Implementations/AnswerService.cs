using AutoMapper;
using System.Threading.Tasks;
using zity.DTOs.Answers;
using zity.ExceptionHandling.Exceptions;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;

namespace zity.Services.Implementations
{
    public class AnswerService(IAnswerRepository answerRepository, IMapper mapper) : IAnswerService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAnswerRepository _answerRepository = answerRepository;

        public async Task<PaginatedResult<AnswerDTO>> GetAllAsync(AnswerQueryDTO queryParam)
        {
            var pageAnswers = await _answerRepository.GetAllAsync(queryParam);
            var answers = pageAnswers.Contents.Select(_mapper.Map<AnswerDTO>).ToList();
            return new PaginatedResult<AnswerDTO>(
                answers,
                pageAnswers.TotalItems,
                pageAnswers.Page,
                pageAnswers.PageSize);
        }


        public async Task<AnswerDTO> GetByIdAsync(int id, string? includes)
        {
            var answer = await _answerRepository.GetByIdAsync(id, includes) 
                ?? throw new EntityNotFoundException(nameof(Answer), id);
            return _mapper.Map<AnswerDTO>(answer);
        }
        public async Task<AnswerDTO> CreateAsync(AnswerCreateDTO createDTO)
        {
            var answer = _mapper.Map<Answer>(createDTO);
            return _mapper.Map<AnswerDTO>(await _answerRepository.CreateAsync(answer));
        }
        public async Task<AnswerDTO> UpdateAsync(int id, AnswerUpdateDTO updateDTO)
        {
            var existingAnswer = await _answerRepository.GetByIdAsync(id, null)
                ?? throw new EntityNotFoundException(nameof(Answer), id);
            _mapper.Map(updateDTO, existingAnswer);
            var updatedAnswer = await _answerRepository.UpdateAsync(existingAnswer);
            return _mapper.Map<AnswerDTO>(updatedAnswer);
        }

        public async Task<AnswerDTO> PatchAsync(int id, AnswerPatchDTO patchDTO)
        {
            var existingAnswer = await _answerRepository.GetByIdAsync(id, null) 
                ?? throw new EntityNotFoundException(nameof(Answer), id);
            _mapper.Map(patchDTO, existingAnswer);
            var patchedAnswer = await _answerRepository.UpdateAsync(existingAnswer);
            return _mapper.Map<AnswerDTO>(patchedAnswer);
        }

        public async Task DeleteAsync(int id)
        {
            await _answerRepository.DeleteAsync(id);
        }

    }
}
