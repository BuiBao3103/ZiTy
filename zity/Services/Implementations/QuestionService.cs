using AutoMapper;
using System.Threading.Tasks;
using zity.DTOs.Questions;
using zity.ExceptionHandling.Exceptions;
using zity.Mappers;
using zity.Models;
using zity.Repositories.Interfaces;
using zity.Services.Interfaces;
using zity.Utilities;
namespace zity.Services.Implementations
{
    public class QuestionService(IQuestionRepository questionRepository, IMapper mapper) : IQuestionService
    {
        readonly IMapper _mapper = mapper;
        private readonly IQuestionRepository _questionRepository = questionRepository;

        public async Task<PaginatedResult<QuestionDTO>> GetAllAsync(QuestionQueryDTO queryParam)
        {
            var pageQuestions = await _questionRepository.GetAllAsync(queryParam);
            var questions = pageQuestions.Contents.Select(_mapper.Map<QuestionDTO>).ToList();
            return new PaginatedResult<QuestionDTO>(
                questions,
                pageQuestions.TotalItems,
                pageQuestions.Page,
                pageQuestions.PageSize);
        }


        public async Task<QuestionDTO> GetByIdAsync(int id, string? includes = null)
        {
            var question = await _questionRepository.GetByIdAsync(id, includes)
                    ?? throw new EntityNotFoundException(nameof(Question), id);
            return _mapper.Map<QuestionDTO>(question);
        }
        public async Task<QuestionDTO> CreateAsync(QuestionCreateDTO createDTO)
        {
            var question = _mapper.Map<Question>(createDTO);
            return _mapper.Map<QuestionDTO>(await _questionRepository.CreateAsync(question));
        }
        public async Task<QuestionDTO> UpdateAsync(int id, QuestionUpdateDTO updateDTO)
        {
            var existingQuestion = await _questionRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(Question), id);
            _mapper.Map(updateDTO, existingQuestion);
            var updatedQuestion = await _questionRepository.UpdateAsync(existingQuestion);
            return _mapper.Map<QuestionDTO>(updatedQuestion);
        }

        public async Task<QuestionDTO> PatchAsync(int id, QuestionPatchDTO patchDTO)
        {
            var existingQuestion = await _questionRepository.GetByIdAsync(id)
                    ?? throw new EntityNotFoundException(nameof(Question), id);
            _mapper.Map(patchDTO, existingQuestion);
            var patchedQuestion = await _questionRepository.UpdateAsync(existingQuestion);
            return _mapper.Map<QuestionDTO>(patchedQuestion);
        }

        public async Task DeleteAsync(int id)
        {
            await _questionRepository.DeleteAsync(id);
        }

    }
}